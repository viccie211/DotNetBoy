using System.Diagnostics;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Models.Cartridges;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class MmuService : IMmuService
{
    private readonly IByteUshortService _byteUshortService;
    private readonly ITimerService _timerService;

    public MmuService(IByteUshortService byteUshortService, ITimerService timerService)
    {
        _byteUshortService = byteUshortService;
        _timerService = timerService;
        MappedMemory = new byte[ushort.MaxValue + 1];
        MappedMemory[AddressConsts.LY_REGISTER_ADDRESS] = 0x00;
        Cartridge = new DefaultCartridge(new byte[0x8000]);
    }

    public byte[] MappedMemory { get; init; }
    public EMbcType MbcType { get; set; } = EMbcType.NoMbc;
    public ICartridge Cartridge { get; set; }
    
    public byte ReadByte(ushort address)
    {
        if (address is <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS or (>= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS))
        {
            return Cartridge.ReadByte(address);
        }

        if (address == AddressConsts.DIV_REGISTER)
            return _timerService.Div;

        if (address == AddressConsts.TIMA_REGISTER)
            return _timerService.Tima;

        if (address == AddressConsts.TMA_REGISTER)
            return _timerService.Tma;

        if (address == AddressConsts.TAC_REGISTER)
            return _timerService.Tac;

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
        if (address is >= 0xC000 and <= 0xDDFF)
            //Also write to echo WRAM
            MappedMemory[(ushort)(address + 0x2000)] = value;
        if (address is >= 0xE000 and <= 0xFDFF)
            //Also write to normal WRAM
            MappedMemory[(ushort)(address - 0x2000)] = value;

        if (address == AddressConsts.DIV_REGISTER)
            _timerService.Div = value;

        if (address == AddressConsts.TIMA_REGISTER)
            _timerService.Tima = value;

        if (address == AddressConsts.TMA_REGISTER)
            _timerService.Tma = value;

        if (address == AddressConsts.TAC_REGISTER)
            _timerService.Tac = value;
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
                return MappedMemory[new Range(0x8000, 0x9000)];
            case ETileSet.TileSet1:
                return MappedMemory[new Range(0x8800, 0x9800)];
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
        return MappedMemory[new Range(0xFE00, 0xFE9F)];
    }

    public OamObject[] GetOamObjects()
    {
        const int oamObjectLength = 4;
        const int totalOamObjects = 40;
        var oamRam = GetOamBytes();
        var result = new OamObject[totalOamObjects];

        for (int i = 0; i < totalOamObjects; i++)
        {
            result[i] = oamRam.Skip(i * oamObjectLength).Take(oamObjectLength).ToArray();
        }

        return result;
    }
}