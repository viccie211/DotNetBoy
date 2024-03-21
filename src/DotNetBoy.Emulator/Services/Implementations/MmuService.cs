using System.Diagnostics;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class MmuService : IMmuService
{
    private readonly IByteUshortService _byteUshortService;

    public MmuService(IByteUshortService byteUshortService)
    {
        _byteUshortService = byteUshortService;
        MappedMemory = new byte[ushort.MaxValue + 1];
        MappedMemory[0xFF44] = 0x90;
    }

    public byte[] MappedMemory { get; init; }

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
            //Can't write to ROM
            return;
        MappedMemory[address] = value;
        if (address is >= 0xC000 and <= 0xDDFF)
            //Also write to echo WRAM
            MappedMemory[(ushort)(address + 0x2000)] = value;
        if (address is >= 0xE000 and <= 0xFDFF)
            //Also write to normal WRAM
            MappedMemory[(ushort)(address - 0x2000)] = value;
    }

    public void LoadRom(byte[] rom)
    {
        //Rom length is max 0x7FFF
        for (int i = 0; i < rom.Length && i < 0x7FFF; i++)
        {
            MappedMemory[i] = rom[i];
        }
    }

    public byte[] GetTileSet(TileSet tileSet)
    {
        switch (tileSet)
        {
            default:
            case TileSet.TileSet0:
                return MappedMemory.Skip(0x7FFF).Take(0x1000).ToArray();
            case TileSet.TileSet1:
                return MappedMemory.Skip(0x87FF).Take(0x1000).ToArray();
        }
    }

    public byte[] GetTileMap(TileMap tileMap)
    {
        switch (tileMap)
        {
            default:
            case TileMap.TileMap0:
                return MappedMemory.Skip(0x97FF).Take(0x400).ToArray();
            case TileMap.TileMap1:
                return MappedMemory.Skip(0x9BFF).Take(0x400).ToArray();
        }
    }
}