namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public abstract class ResetBitInstructionsBase
{
    protected byte GetResetMask(int bitNumber)
    {
        byte baseMask = 0xFF;
        byte toShift = 0x01;
        toShift = (byte)(toShift << bitNumber);
        var result = (byte)(baseMask ^ toShift);
        return result;
    }
}