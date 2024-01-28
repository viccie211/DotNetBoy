using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class IncrementInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public IncrementInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x03, IncrementBC },
            { 0x04, IncrementB },
            { 0x33, IncrementStackPointer }
        };
        _clockService = clockService;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public void IncrementBC(ICpuRegistersService registers)
    {
        registers.BC = Increment16Bits(registers.BC, registers);
    }

    /// <summary>
    /// Increment the B register by one. Sets Z 0 H - 
    /// </summary>
    /// Verified with BGB
    public void IncrementB(ICpuRegistersService registers)
    {
        registers.B = Increment8Bits(registers.B, registers);
    }

    public void IncrementStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = Increment16Bits(registers.StackPointer, registers);
    }

    private byte Increment8Bits(byte initial, ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(initial, 0x01);
        var result = (byte)(initial + 1);
        registers.F.Zero = result == 0;
        _clockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }

    private ushort Increment16Bits(ushort initial, ICpuRegistersService registers)
    {
        var result = (ushort)(initial + 1);
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
        return result;
    }
}