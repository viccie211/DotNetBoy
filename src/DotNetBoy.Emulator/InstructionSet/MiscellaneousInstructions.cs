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
            { 0x10, Stop },
            { 0x27, DAA },
            { 0x2F, ComplementA },
            { 0x37, SetCarryFlag },
            { 0x3F, FlipCarryFlag },
            { 0x76, Halt },
            { 0xCB, NOP },
            { 0xD3, NOP },
            { 0xDB, NOP },
            { 0xDD, NOP },
            { 0xE3, NOP },
            { 0xE4, NOP },
            { 0xEB, NOP },
            { 0xEC, NOP },
            { 0xED, NOP },
            { 0xF4, NOP },
            { 0xFC, NOP },
            { 0xFD, NOP },
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

    public void DAA(ICpuRegistersService registers)
    {
        // note: assumes a is a uint8_t and wraps from 0xff to 0
        if (!registers.F.Subtract)
        {
            // after an addition, adjust if (half-)carry occurred or if result is out of bounds
            if (registers.F.Carry || registers.A > 0x99)
            {
                registers.A += 0x60;
                registers.F.Carry = true;
            }

            if (registers.F.HalfCarry || (registers.A & 0x0f) > 0x09)
            {
                registers.A += 0x6;
            }
        }
        else
        {
            // after a subtraction, only adjust if (half-)carry occurred
            if (registers.F.Carry)
            {
                registers.A -= 0x60;
            }

            if (registers.F.HalfCarry)
            {
                registers.A -= 0x6;
            }
        }

        // these flags are always updated
        registers.F.Zero = registers.A == 0; // the usual z flag
        registers.F.HalfCarry = false; // h flag is always cleared
        registers.ProgramCounter += 1;
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

    public void SetCarryFlag(ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.F.Carry = true;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public void FlipCarryFlag(ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.F.Carry = !registers.F.Carry;
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

    /// <summary>
    /// Stop the the CPU with the program counter one higher than the current instruction
    /// TODO: Implement Actual stopping behaviour
    /// </summary>
    /// Verified against BGB
    public void Stop(ICpuRegistersService registers)
    {
        registers.ProgramCounter += 2;
        _clockService.Clock();
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}