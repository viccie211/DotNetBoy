using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Models.Cartridges;

public abstract class CartridgeBase
{
    protected const ushort BANK_SIZE = 0x4000;
    protected int RomLength = 0;
    protected byte[][] RomBanks;
    protected byte[][]? RamBanks;
    protected readonly EMbcType Type;
    protected int RequiredNumberBits = 1;

    protected CartridgeBase(byte[] rom, EMbcType type)
    {
        RomLength = rom.Length;
        var bankCount = BankCount();
        RomBanks = new byte[bankCount][];
        for (int i = 0; i < bankCount; i++)
        {
            var baseAddress = i * AddressConsts.ROM_BANK_1_BASE_ADDRESS;
            RomBanks[i] = rom[baseAddress..(baseAddress + BANK_SIZE)];

        }

        Type = type;
        var romSize = 32 << rom[AddressConsts.CARTRIDGE_SIZE_HEADER_ADDRESS];
        RequiredNumberBits = (romSize >> 4) - 1;
    }

    protected int BankCount() => (RomLength / BANK_SIZE) + (RomLength % BANK_SIZE != 0 ? 1 : 0);
}