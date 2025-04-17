using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IMmuService
{
    byte ReadByte(ushort address, bool bypass = false);

    /// <summary>
    /// Reads two bytes from memory at the specified address. It reads the first byte and stores it in the upper
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    ushort ReadWordLittleEndian(ushort address);

    void WriteByte(ushort address, byte value);

    /// <summary>
    /// Writes a byte raw to the internal mapped memory without all the logic that normally happens when a byte is written by WriteByte
    /// Used for instance to emulate the ClockService writing to the timer registers while WriteByte sets them to 0 on write.
    /// </summary>
    /// <param name="address">The address to write to</param>
    /// <param name="value">The value to write</param>
    void WriteByteRaw(ushort address, byte value);

    void LoadRom(byte[] rom);
    byte[] GetTileSet(ETileSet eTileSet);
    byte[] GetTileMap(ETileMap eTileMap);
    OamObject[] GetOamObjects();
}