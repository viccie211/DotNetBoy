namespace DotNetBoy.Emulator.InstructionSet;

public class JumpInstructions
{
    public static void Jump(Cpu cpu)
    {
        cpu.regPC = cpu.MmuService.ReadWordLittleEndian((ushort)(cpu.regPC + 1));
        cpu.Clock(4);
    }
}