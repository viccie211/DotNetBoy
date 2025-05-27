using System.Diagnostics;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Models.Cartridges;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class MmuService : IMmuService
{
    private readonly IByteUshortService _byteUshortService;
    private readonly ITimerService _timerService;
    private readonly IJoyPadService _joyPadService;

    public MmuService(IByteUshortService byteUshortService, ITimerService timerService, IEventService eventService, IJoyPadService joyPadService)
    {
        _byteUshortService = byteUshortService;
        _timerService = timerService;
        _joyPadService = joyPadService;
        MappedMemory = new byte[ushort.MaxValue + 1];
        MappedMemory[AddressConsts.LY_REGISTER_ADDRESS] = 0x00;
        Cartridge = new DefaultCartridge(new byte[0x8000]);
        eventService.MClock += DoDMA;
        eventService.InterruptRaised += HandleInterruptRaised;
    }

    public byte[] MappedMemory { get; init; }
    public EMbcType MbcType { get; set; } = EMbcType.NoMbc;
    public ICartridge Cartridge { get; set; }

    private ushort _dmaSourceAddress = 0;
    private ushort _dmaCycles = 0;
    private byte _dmaLastByteWritten = 0;

    public byte ReadByte(ushort address, bool bypass = false)
    {
        // if (!bypass && _dmaCycles != 0 && (address < 0xFF80 || address > 0xFFFE))
        // {
        //     //Return garbage data if doing DMA 
        //     return 0xFF;
        // }

        if (address is <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS or (>= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS))
        {
            return Cartridge.ReadByte(address);
        }

        switch (address)
        {
            case AddressConsts.DIV_REGISTER:
                return _timerService.Div;
            case AddressConsts.TIMA_REGISTER:
                return _timerService.Tima;
            case AddressConsts.TMA_REGISTER:
                return _timerService.Tma;
            case AddressConsts.TAC_REGISTER:
                return _timerService.Tac;
            case AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS:
                return (byte)(MappedMemory[AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS] | 0xe0);
            case AddressConsts.JOYPAD_INPUT_REGISTER:
                return _joyPadService.Register;
        }


        return MappedMemory[address];
    }

    public ushort ReadWordLittleEndian(ushort address)
    {
        return _byteUshortService.CombineBytes(ReadByte((ushort)(address + 1)), ReadByte(address));
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address is <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS or (>= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS))
        {
            Cartridge.WriteByte(address, value);
            return;
        }

        MappedMemory[address] = value;
        switch (address)
        {
            //Also write to echo WRAM
            case >= 0xC000 and <= 0xDDFF:
                MappedMemory[(ushort)(address + 0x2000)] = value;
                break;
            //Also write to normal WRAM
            case >= 0xE000 and <= 0xFDFF:
                MappedMemory[(ushort)(address - 0x2000)] = value;
                break;
            case AddressConsts.DIV_REGISTER:
                _timerService.Div = value;
                break;
            case AddressConsts.TIMA_REGISTER:
                _timerService.Tima = value;
                break;
            case AddressConsts.TMA_REGISTER:
                _timerService.Tma = value;
                break;
            case AddressConsts.TAC_REGISTER:
                _timerService.Tac = value;
                break;
            case AddressConsts.JOYPAD_INPUT_REGISTER:
                _joyPadService.SelectDPad = (value & 0x10) == 0;
                _joyPadService.SelectButtons = (value & 0x20) == 0;
                break;
            case AddressConsts.DMA_REGISTER:
                if (_dmaCycles >= 160)
                    break;
                _dmaSourceAddress = (ushort)(value << 8);
                _dmaCycles = 162;
                break;
        }
    }

    /// <inheritdoc/>
    public void WriteByteRaw(ushort address, byte value)
    {
        MappedMemory[address] = value;
    }

    public void LoadRom(byte[] rom)
    {
        MbcType = ReadMbcType(rom);
        if (MbcType is EMbcType.Mbc1 or EMbcType.Mbc1Ram or EMbcType.Mbc1RamBattery)
        {
            Cartridge = new Mbc1Cartridge(rom, MbcType);
            return;
        }

        Cartridge = new DefaultCartridge(rom);
    }

    private EMbcType ReadMbcType(byte[] rom)
    {
        var mbcHeaderByte = rom[AddressConsts.CARTRIDGE_TYPE_HEADER_ADDRESS];
        switch (mbcHeaderByte)
        {
            case 0x01:
                return EMbcType.Mbc1;
            case 0x02:
                return EMbcType.Mbc1Ram;
            case 0x03:
                return EMbcType.Mbc1RamBattery;
            case 0x05:
                return EMbcType.Mbc2;
            case 0x06:
                return EMbcType.Mbc2Battery;
            default:
                //TODO: Implement more MBCs
                return EMbcType.NoMbc;
        }
    }


    public byte[] GetTileSet(ETileSet eTileSet)
    {
        switch (eTileSet)
        {
            default:
            case ETileSet.TileSet0:
                return MappedMemory[new Range(0x8800, 0x9800)];
            case ETileSet.TileSet1:
                return MappedMemory[new Range(0x8000, 0x9000)];
        }
    }

    public byte[] GetTileMap(ETileMap eTileMap)
    {
        switch (eTileMap)
        {
            default:
            case ETileMap.TileMap0:
                return MappedMemory[new Range(0x9800, 0x9C00)];
            case ETileMap.TileMap1:
                return MappedMemory[new Range(0x9C00, 0xA000)];
        }
    }

    public byte[] GetOamBytes()
    {
        int length = AddressConsts.OAM_TOP_ADDRESS + 1 - AddressConsts.OAM_BASE_ADDRESS;
        byte[] oamBytes = new byte[length];
        Array.Copy(MappedMemory, AddressConsts.OAM_BASE_ADDRESS, oamBytes, 0, length);
        return oamBytes;
    }

    public OamObject[] GetOamObjects()
    {
        const int oamObjectLength = 4;
        const int totalOamObjects = 40;
        var oamRam = GetOamBytes();
        var result = new OamObject[totalOamObjects];

        for (int i = 0; i < totalOamObjects; i++)
        {
            byte[] objectBytes = new byte[oamObjectLength];
            Array.Copy(oamRam, i * oamObjectLength, objectBytes, 0, oamObjectLength);
            result[i] = objectBytes;
        }

        return result;
    }

    public void DoDMA(object? sender, ClockEventArgs eventArgs)
    {
        if (_dmaCycles == 0)
            return;

        if (_dmaCycles <= 160)
        {
            byte index = (byte)(160 - _dmaCycles);
            _dmaLastByteWritten = ReadByte((ushort)(_dmaSourceAddress + index), true);
            WriteByteRaw((ushort)(AddressConsts.OAM_BASE_ADDRESS + index), _dmaLastByteWritten);
        }

        _dmaCycles--;
    }

    public void HandleInterruptRaised(object? sender, RaiseInterruptEventArgs interruptEventArgs)
    {
        WriteByteRaw(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, (byte)(MappedMemory[AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS] | interruptEventArgs.InterruptRegister));
    }
}