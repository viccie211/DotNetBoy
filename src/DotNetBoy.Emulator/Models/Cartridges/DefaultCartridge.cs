namespace DotNetBoy.Emulator.Models.Cartridges;

public class DefaultCartridge : ICartridge
{
    private readonly byte[] _rom;

    public DefaultCartridge(byte[] rom)
    {
        _rom = rom;
    }

    public byte ReadByte(ushort address)
    {
        return _rom[address];
    }

    public void WriteByte(ushort address, byte value)
    {
        throw new NotImplementedException();
    }
}