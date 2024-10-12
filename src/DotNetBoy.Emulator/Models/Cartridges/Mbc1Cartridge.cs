using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Models.Cartridges;

public class Mbc1Cartridge : ICartridge
{
    private List<byte[]> _romBanks;
    private List<byte[]> _ramBanks;
    private EMbcType _type;
    private int _activeRomBankLower5Bits = 1;
    private int _activeRamBankOrRomBankUpper2Bits = 0;

    private int _activeRomBank
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

    private int _requiredNumberBits = 1;
    private bool _ramBankingMode = false;
    private bool _ramEnable = false;

    public Mbc1Cartridge(byte[] rom, EMbcType type)
    {
        var bankCount = (rom.Length / ICartridge.BANK_SIZE) + (rom.Length % ICartridge.BANK_SIZE != 0 ? 1 : 0);
        _romBanks = [];
        for (int i = 0; i < bankCount; i++)
        {
            var baseAddress = i * AddressConsts.ROM_BANK_1_BASE_ADDRESS;
            _romBanks.Add(new byte[ICartridge.BANK_SIZE]);
            for (int j = 0; j < ICartridge.BANK_SIZE; j++)
            {
                _romBanks[i][j] = rom[baseAddress + j];
            }
        }

        _type = type;
        var romSize = 32 << rom[AddressConsts.CARTRIDGE_SIZE_HEADER_ADDRESS];
        _requiredNumberBits = (romSize >> 4) - 1;
        if (_type == EMbcType.Mbc1Ram || _type == EMbcType.Mbc1RamBattery)
        {
            _ramBanks = [new byte[0x2000], new byte[0x2000], new byte[0x2000]];
        }
    }


    public byte ReadByte(ushort address)
    {
        if (address <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS && !_ramBankingMode)
        {
            return _romBanks[0][address];
        }
        
        if (address <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS)
        {
            return _romBanks[_activeRamBankOrRomBankUpper2Bits][address];
        }

        if (address is >= AddressConsts.ROM_BANK_1_BASE_ADDRESS and <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS)
        {
            return _romBanks[_activeRomBank][address - AddressConsts.ROM_BANK_1_BASE_ADDRESS];
        }

        if (_type == EMbcType.Mbc1Ram || _type == EMbcType.Mbc1RamBattery && address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS && _ramEnable)
        {
            if (_ramBankingMode)
            {
                return _ramBanks[_activeRamBankOrRomBankUpper2Bits][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];
            }
            else
            {
                return _ramBanks[0][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];
            }
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
            if (_requiredNumberBits < 5 && (maskedValue & 0x10) == 0x10)
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

        if (_ramEnable && address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS )
        {
            _ramBanks[_activeRamBankOrRomBankUpper2Bits][address-AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS] = value;
        }
    }
}