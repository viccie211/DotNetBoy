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
            { 0xC6, AddD8ToA },
            { 0xCE, AddD8WithCarryToA },
            { 0xD6, SubtractD8FromA }
        };
    }

    /// <summary>
    /// Adds the next byte in memory to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddD8ToA(ICpuRegistersService registers)
    {
        var toAdd = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        AddByteToA(toAdd, registers);
    }

    /// <summary>
    /// Adds the next byte in memory and the carry flag to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    ///
    public void AddD8WithCarryToA(ICpuRegistersService registers)
    {
        var toAdd = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        AddByteWithCarryToA(toAdd, registers);
    }

    /// <summary>
    /// Subtracts the next byte in memory from the A register and stores the result in the A register
    /// Sets Z 1 H C
    /// </summary>
    /// 
    public void SubtractD8FromA(ICpuRegistersService registers)
    {
        var toSubtract = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        SubtractByteFromA(toSubtract, registers);
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

    private void AddByteWithCarryToA(byte toAdd, ICpuRegistersService registers)
    {
        if (registers.F.Carry)
        {
            toAdd += 1;
        }

        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitAddition(toAdd, registers.A);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(toAdd, registers.A);
        registers.A += toAdd;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    private void SubtractByteFromA(byte toSubtract, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtraction(registers.A, toSubtract);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(registers.A, toSubtract);
        registers.A -= toSubtract;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = true;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }
}