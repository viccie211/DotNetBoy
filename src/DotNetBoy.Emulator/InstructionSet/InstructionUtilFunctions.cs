namespace DotNetBoy.Emulator.InstructionSet;

public class InstructionUtilFunctions
{

    /// <summary>
    /// Calculate the HalfCarry for an 8 bit addition
    /// </summary>
    /// <param name="a">The left side of the addition</param>
    /// <param name="b">The right side of the addition</param>
    /// <returns>Whether half carry should be set</returns>
    public static bool HalfCarryFor8BitAddition(byte a, byte b)
    {
        return (((a & 0xF) + (b & 0xF)) & 0x10) == 0x10;
    }

    /// <summary>
    /// Calculate the half carry for an 8 bit subtraction
    /// </summary>
    /// <param name="a">The normal left side of the subtraction</param>
    /// <param name="b">The right side of the subtraction. This should be a positive byte, it will be made "negative" in the method</param>
    /// <returns>Whether half carry should be set</returns>
    public static bool HalfCarryFor8BitSubtraction(byte a, byte b)
    {
        return (((a & 0xF) - (b & 0xF)) & 0x10) == 0x10;
    }

    public static bool CarryFor8BitAddition(byte a, byte b)
    {
        return a + b > byte.MaxValue;
    }

    public static bool CarryFor8BitSubtraction(byte a, byte b)
    {
        return a < b;
    }


    public static ushort SignedAdd(ushort target, byte add)
    {
        var s = ByteToSignedByte(add);
        return (ushort)(target + s);
    }

    public static sbyte ByteToSignedByte(byte input)
    {
        sbyte s = unchecked((sbyte)input);
        return s;
    }

    public static ushort NextAddress(ushort programCounter)
    {
        return (ushort)(programCounter + 1);
    }

    public static bool CarryFor16BitAddition(ushort a, ushort b)
    {
        return (a + b) > ushort.MaxValue;
    }

    public static bool HalfCarryFor16BitAddition(ushort a, ushort b)
    {
        return ((a & 0xFFF) + (b & 0xFFF)) > 0xFFF;
    }

}