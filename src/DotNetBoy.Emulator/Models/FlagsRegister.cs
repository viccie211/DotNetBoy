using System.Diagnostics.CodeAnalysis;

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
        FlagsRegister result = new FlagsRegister
        {
            Zero = (0b10000000 & input) != 0,
            Subtract = (0b01000000 & input) != 0,
            HalfCarry = (0b00100000 & input) != 0,
            Carry = (0b00010000 & input) != 0
        };
        return result;
    }

    public override bool Equals(object? toEqual)
    {
        if (!(toEqual is FlagsRegister) && !(toEqual is byte))
        {
            return false;
        }

        if (toEqual is FlagsRegister asRegister)
        {
            return Equals(asRegister);
        }

        if (toEqual is byte asByte)
        {
            return asByte == (byte)this;
        }

        return false;
    }

    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        return HashCode.Combine(Zero, Subtract, HalfCarry, Carry);
    }

    protected bool Equals(FlagsRegister other)
    {
        return Zero == other.Zero && Subtract == other.Subtract && HalfCarry == other.HalfCarry && Carry == other.Carry;
    }

    public override string ToString()
    {
        var asByte = (byte)this;
        return $"0x{asByte:X2}: {(Zero?"Z":"!Z")} {(Subtract?"S":"!S")} {(HalfCarry?"H":"!H")} {(Carry?"C":"!C")}";
    }
}