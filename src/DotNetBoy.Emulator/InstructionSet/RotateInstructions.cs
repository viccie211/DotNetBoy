using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateInstructions : RotateInstructionsBase, IInstructionSet
{
    private readonly IClockService _clockService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateInstructions(IClockService clockService) : base(clockService)
    {
        _clockService = clockService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x07, RotateALeft },
            { 0x0F, RotateARight },
            { 0x17, RotateALeftThroughCarry },
            { 0x1F, RotateARightThroughCarry }
        };
    }

    public void RotateALeft(ICpuRegistersService registers)
    {
        registers.A = RotateByteLeft(registers.A, registers);
    }

    public void RotateALeftThroughCarry(ICpuRegistersService registers)
    {
        registers.A = RotateByteLeftThroughCarry(registers.A, registers);
    }


    public void RotateARight(ICpuRegistersService registers)
    {
        registers.A = RotateByteRight(registers.A, registers);
    }

    public void RotateARightThroughCarry(ICpuRegistersService registers)
    {
        registers.A = RotateByteRightThroughCarry(registers.A, registers);
    }
}