using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public abstract class BitInstructionsBase(IClockService clockService)
{
    protected readonly IClockService ClockService = clockService;

    protected void SetComplementOfBitToZeroFlag(int bitNumber, byte input, ICpuRegistersService registers)
    {
        var shifted = (byte)(input >> bitNumber);
        var masked = (byte)(shifted & 0x01);
        registers.F.Zero = masked != 1;
        registers.F.Subtract = false;
        registers.F.HalfCarry = true;
        clockService.Clock();
        registers.ProgramCounter += 2;
    }
}