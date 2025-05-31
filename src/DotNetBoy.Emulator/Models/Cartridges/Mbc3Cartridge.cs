using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Models.Cartridges;

public class Mbc3Cartridge : CartridgeBase, ICartridge
{
    private int _activeRomBank = 0;
    private int _activeRamBank = 0;
    private RamTimerState _ramTimerState = RamTimerState.Ram;
    private bool _ramEnable = false;
    private DateTime _startDate = DateTime.Now;
    private int ActiveRomBank => _activeRomBank == 0 ? 1 : _activeRomBank;

    public Mbc3Cartridge(byte[] rom, EMbcType type) : base(rom, type)
    {
        if (Type == EMbcType.Mbc3Ram || Type == EMbcType.Mbc3RamBattery)
        {
            RamBanks = new byte[8][];
            for (int i = 0; i < 8; i++)
            {
                RamBanks[i] = new byte[0x2000];
            }
        }
    }

    public byte ReadByte(ushort address)
    {
        if (address <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS)
        {
            return RomBanks[0][address];
        }

        if (address is >= AddressConsts.ROM_BANK_1_BASE_ADDRESS and <= AddressConsts.ROM_BANK_1_UPPER_ADDRESS)
        {
            return RomBanks[ActiveRomBank][address - AddressConsts.ROM_BANK_1_BASE_ADDRESS];
        }

        if (_ramEnable && Type is EMbcType.Mbc3Ram or EMbcType.Mbc3RamBattery or EMbcType.Mbc3TimerBattery or EMbcType.Mbc3TimerRamBattery &&
            address is >= AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS and <= AddressConsts.CARTRIDGE_RAM_UPPER_ADDRESS)
        {
            var timeSinceStart = DateTime.Now - _startDate;
            switch (_ramTimerState)
            {
                case RamTimerState.Ram when Type is EMbcType.Mbc3Ram or EMbcType.Mbc3RamBattery or EMbcType.Mbc3TimerRamBattery:
                    if (RamBanks != null) return RamBanks[_activeRamBank][address - AddressConsts.CARTRIDGE_RAM_BASE_ADDRESS];
                    break;
                // case RamTimerState.Seconds when Type is EMbcType.Mbc3TimerBattery or EMbcType.Mbc3TimerRamBattery:
                //     return (byte)timeSinceStart.Seconds;
                // case RamTimerState.Minutes when Type is EMbcType.Mbc3TimerBattery or EMbcType.Mbc3TimerRamBattery:
                //     return (byte)timeSinceStart.Minutes;
                // case RamTimerState.Hours when Type is EMbcType.Mbc3TimerBattery or EMbcType.Mbc3TimerRamBattery:
                //     return (byte)timeSinceStart.Hours;
                // case RamTimerState.DaysLow when Type is EMbcType.Mbc3TimerBattery or EMbcType.Mbc3TimerRamBattery:
                //     
            }
        }

        return 0xFF;
    }

    public void WriteByte(ushort address, byte value)
    {
        if (address <= 0x1FFF)
        {
            _ramEnable = (value & 0x0A) == 0x0A;
        }

        if (address is >= 0x2000 and <= AddressConsts.ROM_BANK_0_UPPER_ADDRESS)
        {
            _activeRomBank = value & 0x7F;
        }

        if (address is >= 0x4000 and <= 0x5FFF)
        {
            switch (value)
            {
                case <= 0x7:
                    _ramTimerState = RamTimerState.Ram;
                    _activeRamBank = value;
                    break;

                case 0x8 when Type == EMbcType.Mbc3TimerBattery:
                    _ramTimerState = RamTimerState.Seconds;
                    break;
                case 0x9 when Type == EMbcType.Mbc3TimerBattery:
                    _ramTimerState = RamTimerState.Minutes;
                    break;
                case 0xA when Type == EMbcType.Mbc3TimerBattery:
                    _ramTimerState = RamTimerState.Hours;
                    break;
                case 0xB when Type == EMbcType.Mbc3TimerBattery:
                    _ramTimerState = RamTimerState.DaysLow;
                    break;
                case 0xC when Type == EMbcType.Mbc3TimerBattery:
                    _ramTimerState = RamTimerState.DaysHighAndStatus;
                    break;
            }
        }
    }

    private enum RamTimerState
    {
        Ram,
        Seconds,
        Minutes,
        Hours,
        DaysLow,
        DaysHighAndStatus
    }
}