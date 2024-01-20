using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class IncrementInstructions : IInstructionSet
{
    public IncrementInstructions()
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x03, IncrementBC },
            { 0x04, IncrementB },
            { 0x33, IncrementStackPointer }
        };
    }

    private void IncrementBC(CpuRegisters cpu)
    {
        cpu.BC++;
        cpu.Clock(2);
        cpu.ProgramCounter += 1;
    }

    private void IncrementB(CpuRegisters cpu)
    {
        cpu.F.Subtract = false;
        cpu.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(cpu.B, 1);
        cpu.B++;
        cpu.F.Zero = cpu.B == 0;
        cpu.Clock();
        cpu.ProgramCounter += 1;
    }

    private void IncrementStackPointer(CpuRegisters cpu)
    {
        cpu.StackPointer++;
        cpu.Clock(2);
        cpu.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}