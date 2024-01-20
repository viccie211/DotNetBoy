using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class IncrementInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    public IncrementInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x03, IncrementBC },
            { 0x04, IncrementB },
            { 0x33, IncrementStackPointer }
        };
        _clockService = clockService;
    }

    private void IncrementBC(CpuRegisters cpu)
    {
        cpu.BC++;
        _clockService.Clock(2);
        cpu.ProgramCounter += 1;
    }

    private void IncrementB(CpuRegisters cpu)
    {
        cpu.F.Subtract = false;
        cpu.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(cpu.B, 1);
        cpu.B++;
        cpu.F.Zero = cpu.B == 0;
        _clockService.Clock();
        cpu.ProgramCounter += 1;
    }

    private void IncrementStackPointer(CpuRegisters cpu)
    {
        cpu.StackPointer++;
        _clockService.Clock(2);
        cpu.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}