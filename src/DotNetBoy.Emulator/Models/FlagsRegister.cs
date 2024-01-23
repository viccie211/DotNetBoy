namespace DotNetBoy.Emulator.Models;

public class FlagsRegister
{
    public bool Zero { get; set; }
    public bool Subtract { get; set; }
    public bool HalfCarry { get; set; }
    public bool Carry { get; set; }

    public static implicit operator byte(FlagsRegister flagsRegister)
    {
        byte result = 0;
        result = (byte)(flagsRegister.Zero ? 0b10000000 | result : result);
        result = (byte)(flagsRegister.Subtract ? 0b01000000 | result : result);
        result = (byte)(flagsRegister.HalfCarry ? 0b00100000 | result : result);
        result = (byte)(flagsRegister.Carry ? 0b00010000 | result : result);
        return result;
    }

    public static implicit operator FlagsRegister(byte input)
    {
        FlagsRegister result = new FlagsRegister();
        result.Zero = ((0b10000000 & input) != 0);
        result.Subtract = ((0b01000000 & input) != 0);
        result.HalfCarry = ((0b00100000 & input) != 0);
        result.Carry = ((0b00010000 & input) != 0);
        return result;
    }
}