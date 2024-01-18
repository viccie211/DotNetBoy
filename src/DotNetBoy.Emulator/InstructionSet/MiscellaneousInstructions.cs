using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions : IInstructionSet
{
    public MiscellaneousInstructions()
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>
        {
            { 0x00, NOP },
            { 0xF3, DisableInterrupts }
        };
    }

    private void NOP(CpuRegisters cpu)
    {
        cpu.ProgramCounter++;
        cpu.Clock();
    }

    private void DisableInterrupts(CpuRegisters cpu)
    {
        cpu.InteruptMasterEnable = false;
        cpu.Clock();
        cpu.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}