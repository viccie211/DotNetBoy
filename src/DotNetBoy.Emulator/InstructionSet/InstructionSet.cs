namespace DotNetBoy.Emulator.InstructionSet;

public class InstructionSetMethods
{
    public static Action<Cpu>[] CreateNonPrefixedInstructionSet()
    {
        var result = CreateArrayFilledWithNOP();
        result[0x00] = MiscellaneousInstructions.NOP;
        result[0x01] = LoadInstructions.LoadD16IntoBC;
        result[0x02] = LoadInstructions.LoadAtAddressBCIntoA;
        result[0x03] = IncrementInstructions.IncrementBC;
        result[0x04] = IncrementInstructions.IncrementB;
        result[0x05] = DecrementInstructions.DecrementB;
        result[0x06] = LoadInstructions.LoadD8IntoB;
        result[0x07] = RotateAndShiftInstructions.RotateLeftWithCarryA;
        result[0x08] = StoreInstructions.StoreStackPointerAtAddress;

        result[0x31] = LoadInstructions.LoadD16IntoSP;
        result[0x33] = IncrementInstructions.IncrementSP;
            
        result[0xC3] = JumpInstructions.Jump;

        result[0xF0] = LoadInstructions.LoadSomeValueIntoA;
        result[0xF3] = MiscellaneousInstructions.DisableInterrupts;
        return result;
    }

    public static Action<Cpu>[] CreatePrefixedInstructionSet()
    {
        var result = CreateArrayFilledWithNOP();
        return result;
    }

    private static Action<Cpu>[] CreateArrayFilledWithNOP()
    {
        var result = new Action<Cpu>[0xFF];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = MiscellaneousInstructions.NOP;
        }

        return result;
    }
}