using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    public MiscellaneousInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>
        {
            { 0x00, NOP },
            { 0xF3, DisableInterrupts }
        };
        _clockService = clockService;
    }

    private void NOP(CpuRegisters cpu)
    {
        cpu.ProgramCounter++;
        _clockService.Clock();
    }

    private void DisableInterrupts(CpuRegisters cpu)
    {
        cpu.InteruptMasterEnable = false;
        _clockService.Clock();
        cpu.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}