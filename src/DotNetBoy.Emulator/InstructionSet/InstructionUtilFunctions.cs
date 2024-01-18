namespace DotNetBoy.Emulator.InstructionSet;

public class InstructionUtilFunctions
{
    public static bool HalfCarryFor8Bit(byte a, byte b)
    {
        return (((a & 0xF) + (b & 0xF)) & 0x10) == 0x10;
    }
}