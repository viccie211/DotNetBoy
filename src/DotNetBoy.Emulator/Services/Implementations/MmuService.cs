﻿using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class MmuService : IMmuService
{
    private readonly IByteUshortService _byteUshortService;

    public MmuService(IByteUshortService byteUshortService)
    {
        _byteUshortService = byteUshortService;
    }

    private byte[] mappedMemory = new byte[ushort.MaxValue];

    public byte ReadByte(ushort address)
    {
        return mappedMemory[address];
    }

    public ushort ReadWordLittleEndian(ushort address)
    {
        return _byteUshortService.CombineBytes(ReadByte((ushort)(address + 1)),ReadByte(address));
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address <= 0x7FFF)
            //Can't write to ROM
            return;
        mappedMemory[address] = value;
        if(address is >= 0xC000 and <= 0xDDFF)
            //Also write to echo WRAM
            mappedMemory[(ushort)(address+0x2000)] = value;
        if(address is >= 0xE000 and <= 0xFDFF)
            //Also write to normal WRAM
            mappedMemory[(ushort)(address-0x2000)] = value;
    }

    public void LoadRom(byte[] rom)
    {
        //Rom length is max 0x7FFF
        for (int i = 0; i < rom.Length && i < 0x7FFF; i++)
        {
            mappedMemory[i] = rom[i];
        }
    }
}