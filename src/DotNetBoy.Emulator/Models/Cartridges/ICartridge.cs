namespace DotNetBoy.Emulator.Models.Cartridges;

public interface ICartridge
{
    public const ushort BANK_SIZE = 0x4000;
    public const ushort BANK_0_BASE_ADDRESS = 0x0000;
    private const ushort BANK_1_BASE_ADDRESS = 0x4000;

    byte ReadByte(ushort address);
    void WriteByte(ushort address, byte value);
}