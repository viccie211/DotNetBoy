using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public MiscellaneousInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>
        {
            { 0x00, NOP },
            { 0x2F, ComplementA },
            { 0x76, Halt },
            { 0xF3, DisableInterrupts },
            { 0xFB, EnableInterrupts },
        };
        _clockService = clockService;
    }

    /// <summary>
    /// Does nothing but increment the Program Counter and pump the clock one cycle
    /// </summary>
    /// Verified against BGB
    public void NOP(ICpuRegistersService registers)
    {
        registers.ProgramCounter++;
        _clockService.Clock();
    }

    public void ComplementA(ICpuRegistersService registers)
    {
        registers.A = (byte)~registers.A;
        registers.F.Subtract = true;
        registers.F.HalfCarry = true;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Sets the InterruptMasterEnable to false
    /// </summary>
    /// Verified against BGB
    public void DisableInterrupts(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void EnableInterrupts(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = true;
        registers.InterruptsJustEnabled = true;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Halt the CPU with the program counter one higher than the current instruction
    /// TODO: Implement exiting a halt state from interrupt or reset
    /// </summary>
    /// Verified against BGB
    public void Halt(ICpuRegistersService registers)
    {
        registers.Halted = true;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}