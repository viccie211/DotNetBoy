using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Models.Cartridges;

public class Mbc1Cartridge : CartridgeBase, ICartridge
{
    private int _activeRomBankLower5Bits = 1;
    private int _activeRamBankOrRomBankUpper2Bits = 0;

    private int ActiveRomBank
    {
        get
        {
            if (_ramBankingMode)
            {
                return _activeRomBankLower5Bits;
            }

            return (_activeRamBankOrRomBankUpper2Bits << 5) + _activeRomBankLower5Bits;
        }
    }


    private bool _ramBankingMode = false;
    private bool _ramEnable = false;

    public Mbc1Cartridge(byte[] rom, EMbcType type) : base(rom, type)
    {
        if (Type == EMbcType.Mbc1Ram || Type == EMbcType.Mbc1RamBattery)
        {
            RamBanks = [new byte[0x2000], new byte[0x2000], new byte[0x2000]];
        }
    }

    public byte ReadByte(ushort address)
    {
        if (address <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS && !_ramBankingMode)
        {
            return RomBanks[0][address];
        }

        if (address <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS)
        {
            return RomBanks[_activeRamBankOrRomBankUpper2Bits][address];
        }

        if (address is >= AddressConsts.ROM_BANK_1_BASE_ADDRESS and <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS)
        {
            return RomBanks[ActiveRomBank][address - AddressConsts.ROM_BANK_1_BASE_ADDRESS];
        }

        if (Type == EMbcType.Mbc1Ram || Type == EMbcType.Mbc1RamBattery && address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS && _ramEnable)
        {
            if (RamBanks == null)
                RamBanks = [];

            if (_ramBankingMode)
                return RamBanks[_activeRamBankOrRomBankUpper2Bits][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];

            return RamBanks[0][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];
        }

        throw new NotImplementedException();
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address <= 0x1FFF)
        {
            _ramEnable = (value & 0x0A) == 0x0A;
        }

        if (address is >= 0x2000 and <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS)
        {
            var maskedValue = (byte)(value & 0x1F);
            if (RequiredNumberBits < 5 && (maskedValue & 0x10) == 0x10)
            {
                _activeRomBankLower5Bits = 0;
            }
            else if (maskedValue == 0)
            {
                _activeRomBankLower5Bits = 1;
            }
            else
            {
                _activeRomBankLower5Bits = maskedValue;
            }
        }

        if (address is >= 0x4000 and <= 0x5FFF)
        {
            var maskedValue = value & 0x03;
            _activeRamBankOrRomBankUpper2Bits = maskedValue;
        }

        if (address is >= 0x6000 and <= 0x7FFF)
        {
            _ramBankingMode = (value & 0x01) == 0x01;
        }

        if (_ramEnable && address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS)
        {
            RamBanks[_activeRamBankOrRomBankUpper2Bits][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS] = value;
        }
    }
}