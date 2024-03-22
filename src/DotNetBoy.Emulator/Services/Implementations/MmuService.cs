using System.Diagnostics;
using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
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

    public byte[] GetTileSet(ETileSet eTileSet)
    {
        switch (eTileSet)
        {
            default:
            case ETileSet.TileSet0:
                return MappedMemory.Skip(0x7FFF).Take(0x1000).ToArray();
            case ETileSet.TileSet1:
                return MappedMemory.Skip(0x87FF).Take(0x1000).ToArray();
        }
    }

    public byte[] GetTileMap(ETileMap eTileMap)
    {
        switch (eTileMap)
        {
            default:
            case ETileMap.TileMap0:
                return MappedMemory.Skip(0x97FF).Take(0x400).ToArray();
            case ETileMap.TileMap1:
                return MappedMemory.Skip(0x9BFF).Take(0x400).ToArray();
        }
    }

    public byte[] GetOamBytes()
    {
        return MappedMemory.Skip(0xFDFF).Take(0x9F).ToArray();
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