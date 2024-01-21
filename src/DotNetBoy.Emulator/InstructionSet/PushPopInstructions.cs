using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class PushPopInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    public PushPopInstructions(IClockService clockService, IMmuService mmuService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>
        {
            { 0xF5, PushAF },
        };
        _clockService = clockService;
        _mmuService = mmuService;
    }

    private void PushAF(CpuRegisters cpu)
    {
        cpu.StackPointer--;
        _mmuService.WriteByte(cpu.StackPointer, cpu.A);
        cpu.StackPointer--;
        _mmuService.WriteByte(cpu.StackPointer, cpu.F);
        cpu.ProgramCounter+=1;
        _clockService.Clock(4);
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}