using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.Emulator.Models.Cartridges;

public class DefaultCartridge : ICartridge
{
    private readonly byte[] _rom;
    private readonly byte[] _ram;

    public DefaultCartridge(byte[] rom)
    {
        _rom = rom;
        _ram = new byte[0x2000];
    }

    public byte ReadByte(ushort address)
    {
        if (address <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS)
            return _rom[address];
        if (address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS)
            return _ram[address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];
        throw new NotImplementedException();
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS)
        {
            _ram[address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS] = value;
        }
    }
}