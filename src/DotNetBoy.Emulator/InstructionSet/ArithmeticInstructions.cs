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
            { 0x09, AddBCToHL },
            { 0x19, AddDEToHL },
            { 0x29, AddHLToHL },
            { 0x39, AddStackPointerToHL },
            { 0x80, AddBToA },
            { 0x81, AddCToA },
            { 0x82, AddDToA },
            { 0x83, AddEToA },
            { 0x84, AddHToA },
            { 0x85, AddLToA },
            { 0x87, AddAToA },
            { 0x90, SubtractBFromA },
            { 0x91, SubtractCFromA },
            { 0x92, SubtractDFromA },
            { 0x93, SubtractEFromA },
            { 0x94, SubtractHFromA },
            { 0x95, SubtractLFromA },
            { 0x96, SubtractAtAddressHLFromA },
            { 0x97, SubtractAFromA },
            { 0xC6, AddD8ToA },
            { 0xCE, AddD8WithCarryToA },
            { 0xD6, SubtractD8FromA }
        };
    }

    #region Add

    /// <summary>
    /// Adds the contents of the B register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddBToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.B, registers);
    }

    /// <summary>
    /// Adds the contents of the C register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddCToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.C, registers);
    }

    /// <summary>
    /// Adds the contents of the D register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddDToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.D, registers);
    }

    /// <summary>
    /// Adds the contents of the E register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddEToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.E, registers);
    }

    /// <summary>
    /// Adds the contents of the H register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddHToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.H, registers);
    }

    /// <summary>
    /// Adds the contents of the L register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddLToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.L, registers);
    }

    public void AddAtAddressHLToA(ICpuRegistersService registers)
    {
        var toAdd = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        AddByteToA(toAdd, registers);
    }

    /// <summary>
    /// Adds the contents of the A register to the A register and stores the result in the A register
    /// Sets Z 0 H C
    /// </summary>
    /// 
    public void AddAToA(ICpuRegistersService registers)
    {
        AddByteToA(registers.A, registers);
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

    #endregion

    #region Subtract

    public void SubtractBFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.B, registers);
    }

    public void SubtractCFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.C, registers);
    }

    public void SubtractDFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.B, registers);
    }

    public void SubtractEFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.E, registers);
    }

    public void SubtractHFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.H, registers);
    }

    public void SubtractLFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.L, registers);
    }

    public void SubtractAtAddressHLFromA(ICpuRegistersService registers)
    {
        var toSubtract = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        SubtractByteFromA(toSubtract, registers);
    }

    public void SubtractAFromA(ICpuRegistersService registers)
    {
        SubtractByteFromA(registers.A, registers);
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

    #endregion

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

    public void AddBCToHL(ICpuRegistersService registers)
    {
        registers.HL = SixteenBitAddition(registers.BC, registers.HL, registers);
    }

    public void AddDEToHL(ICpuRegistersService registers)
    {
        registers.HL = SixteenBitAddition(registers.DE, registers.HL, registers);
    }

    public void AddHLToHL(ICpuRegistersService registers)
    {
        registers.HL = SixteenBitAddition(registers.HL, registers.HL, registers);
    }

    public void AddStackPointerToHL(ICpuRegistersService registers)
    {
        registers.HL = SixteenBitAddition(registers.StackPointer, registers.HL, registers);
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

    public ushort SixteenBitAddition(ushort a, ushort b, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor16BitAddition(a, b);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor16BitAddition(a, b);
        registers.F.Subtract = false;
        var result = (ushort)(a + b);
        registers.F.Zero = result == 0;
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
        return result;
    }
}