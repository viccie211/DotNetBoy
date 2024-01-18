namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions
{
    public static void NOP(Cpu cpu)
    {
        cpu.regPC++;
        cpu.Clock();
    }

    public static void DisableInterrupts(Cpu cpu)
    {
        cpu.interuptMasterEnable = false;
        cpu.Clock();
        cpu.regPC += 1;
    }
}