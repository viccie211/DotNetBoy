using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public abstract class ResetBitInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;
    protected byte ResetBit(int bitNumber, byte toMask, ICpuRegistersService registers)
    {
        byte baseMask = 0xFF;
        byte toShift = 0x01;
        toShift = (byte)(toShift << bitNumber);
        var mask =(byte)(baseMask ^ toShift);
        var result = (byte)(toMask & mask);
        ClockService.Clock(2);
        registers.ProgramCounter += 2;
        return result;
    }
}