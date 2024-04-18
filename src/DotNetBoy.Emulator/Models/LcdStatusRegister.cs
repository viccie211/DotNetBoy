using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Models;

public class LcdStatusRegister
{
    public PpuModes PpuMode { get; set; }
    public bool LycEqualsLy { get; set; }
    public bool Mode0IntSelect { get; set; }
    public bool Mode1IntSelect { get; set; }
    public bool Mode2IntSelect { get; set; }
    public bool LycIntSelect { get; set; }


    public static implicit operator byte(LcdStatusRegister lcdStatusRegister)
    {
        byte result = 0;
        result += (byte)lcdStatusRegister.PpuMode;
        result = (byte)(lcdStatusRegister.LycEqualsLy ? 0b00000100 | result : result);
        result = (byte)(lcdStatusRegister.Mode0IntSelect ? 0b00001000 | result : result);
        result = (byte)(lcdStatusRegister.Mode1IntSelect ? 0b00010000 | result : result);
        result = (byte)(lcdStatusRegister.Mode2IntSelect ? 0b00100000 | result : result);
        result = (byte)(lcdStatusRegister.LycIntSelect ? 0b01000000 | result : result);

        return result;
    }

    public static implicit operator LcdStatusRegister(byte input)
    {
        LcdStatusRegister result = new LcdStatusRegister
        {
            PpuMode = (PpuModes)(0b00000011 & input),
            LycEqualsLy = (0b00000100 & input) != 0,
            Mode0IntSelect = (0b00001000 & input) != 0,
            Mode1IntSelect = (0b00010000 & input) != 0,
            Mode2IntSelect = (0b00100000 & input) != 0,
            LycIntSelect = (0b01000000 & input) != 0,
        };
        return result;
    }

    public LcdStatusRegister Clone()
    {
        return new LcdStatusRegister()
        {
            PpuMode = PpuMode,
            LycEqualsLy = LycEqualsLy,
            Mode0IntSelect = Mode0IntSelect,
            Mode1IntSelect = Mode1IntSelect,
            Mode2IntSelect = Mode2IntSelect,
            LycIntSelect = LycIntSelect
        };
    }
}