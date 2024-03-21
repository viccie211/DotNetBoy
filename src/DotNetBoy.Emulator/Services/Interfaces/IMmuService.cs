﻿using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IMmuService
{
    byte ReadByte(ushort address);

    /// <summary>
    /// Reads two bytes from memory at the specified address. It reads the first byte and stores it in the upper
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    ushort ReadWordLittleEndian(ushort address);

    void WriteByte(ushort address, byte value);
    void LoadRom(byte[] rom);
    byte[] GetTileSet(TileSet tileSet);
    byte[] GetTileMap(TileMap tileMap);
}