using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public abstract class SetBitInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;

    protected byte SetBit(int bitNumber, byte toMask, ICpuRegistersService registers)
    {
        byte toShift = 0x01;
        var mask = (byte)(toShift << bitNumber);
        var result = (byte)(toMask | mask);
        ClockService.Clock(2);
        registers.ProgramCounter += 2;
        return result;
    }
}