using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.Emulator.Models.Cartridges;

public interface ICartridge
{
    public const ushort BANK_SIZE = 0x4000;
    public const ushort BANK_0_BASE_ADDRESS = AddressConsts.ROM_BANK_0_BASE_ADDRESS;
    private const ushort BANK_1_BASE_ADDRESS = AddressConsts.ROM_BANK_1_BASE_ADDRESS;

    byte ReadByte(ushort address);
    void WriteByte(ushort address, byte value);
}