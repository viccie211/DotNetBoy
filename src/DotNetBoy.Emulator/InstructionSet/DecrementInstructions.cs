using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class DecrementInstructions : IInstructionSet
{
    private void DecrementB(CpuRegisters cpu)
    {
        cpu.F.Subtract = true;
        cpu.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(cpu.B, 0xFE);
        cpu.B--;
        cpu.F.Zero = cpu.B == 0;
        cpu.Clock();
        cpu.ProgramCounter += 1;
    }

    public DecrementInstructions()
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x05, DecrementB }
        };
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}