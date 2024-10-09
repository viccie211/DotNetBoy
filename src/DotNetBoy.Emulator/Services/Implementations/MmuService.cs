using System.Diagnostics;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class MmuService : IMmuService
{
    private readonly IByteUshortService _byteUshortService;
    private const int BANK_SIZE = 0x3FFF;
    private const int BANK_0_BASE_ADDRESS = 0x0000;
    private const int BANK_1_BASE_ADDRESS = 0x3FFF;

    public MmuService(IByteUshortService byteUshortService)
    {
        _byteUshortService = byteUshortService;
        MappedMemory = new byte[ushort.MaxValue + 1];
        MappedMemory[AddressConsts.LY_REGISTER_ADDRESS] = 0x00;
        RomBanks=[];
    }

    public byte[] MappedMemory { get; init; }
    public byte[][] RomBanks { get; set; }
    public EMbcType MbcType { get; set; } = EMbcType.NoMbc;

    public byte ReadByte(ushort address)
    {
        return MappedMemory[address];
    }

    public ushort ReadWordLittleEndian(ushort address)
    {
        return _byteUshortService.CombineBytes(ReadByte((ushort)(address + 1)), ReadByte(address));
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address <= 0x7FFF)
        {
            
            if (address is >= 0x2000 and <= 0x3FFF)
            {
                if (value == 0)
                {
                    LoadBank(1,0x4000);
                }
            }

            return;
        }

        //Can't write to ROM
        MappedMemory[address] = value;
        if (address is >= 0xC000 and <= 0xDDFF)
            //Also write to echo WRAM
            MappedMemory[(ushort)(address + 0x2000)] = value;
        if (address is >= 0xE000 and <= 0xFDFF)
            //Also write to normal WRAM
            MappedMemory[(ushort)(address - 0x2000)] = value;

        if (address == 0xFF04)
            //Writing to this register resets it to zero
            MappedMemory[address] = 0x00;
    }

    /// <inheritdoc/>
    public void WriteByteRaw(ushort address, byte value)
    {
        MappedMemory[address] = value;
    }

    public void LoadRom(byte[] rom)
    {
        var bankCount = (rom.Length / BANK_SIZE) + (rom.Length % BANK_SIZE != 0 ? 1 : 0);
        RomBanks=new byte[bankCount][];
        for (int i = 0; i < bankCount; i++)
        {
            RomBanks[i] = rom.Skip(i).Take(BANK_SIZE).ToArray();
        }

        LoadBank(0, 0x0000);
        LoadBank(1, 0x4000);
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

    private void LoadBank(int bankNumber, ushort baseAddress)
    {
        for (int i = 0; i < BANK_SIZE; i++)
        {
            MappedMemory[baseAddress + i] = RomBanks[bankNumber][i];
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