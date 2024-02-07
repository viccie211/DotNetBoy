using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class ArithmeticInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; init; }
    public ArithmeticInstructions(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC6, AddD8ToA }
        };
    }

    public void AddD8ToA(ICpuRegistersService registers)
    {
        var toAdd = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        AddByteToA(toAdd, registers);
    }

    private void AddByteToA(byte toAdd, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitAddition(toAdd, registers.A);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(toAdd, registers.A);
        registers.A += toAdd;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }
}