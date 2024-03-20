using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LogicInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;

    public LogicInstructions(IMmuService mmuService, IClockService clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xB1, ORCWithA },
            { 0xB7, ORAWithA },
            { 0xA9, XORCWithA },
            { 0xAE, XORAtAddressHLWithA },
            { 0xAF, XORAWithA },
            { 0xB6, ORAtAddressHLWithA },
            { 0xE6, ANDD8WithA },
            { 0xEE, XORD8WithA },
            { 0xFE, CompareD8WithA },
        };
        _clockService = clockService;
    }

    public void CompareD8WithA(ICpuRegistersService registers)
    {
        var d8 = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        var result = (byte)(registers.A - d8);
        registers.F.Subtract = true;
        registers.F.Zero = result == 0;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(registers.A, d8);
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtraction(registers.A, d8);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    /// <summary>
    /// Perform a bit wise XOR with the contents of the C register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORCWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.C, registers);
    }

    public void XORAWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.A, registers);
    }

    /// <summary>
    /// Perform a bit wise XOR with the contents of memory at the address specified by the HL register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORAtAddressHLWithA(ICpuRegistersService registers)
    {
        var toXOR = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        XORByteWithA(toXOR, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the A register and the contents of the A register and store it in the A register
    /// </summary>
    /// Verified with BGB
    public void ORAWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.A, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the C register and the contents of the A register and store it in the A register
    /// </summary>
    /// Verified with BGB
    public void ORCWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.C, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of memory at the address specified by the HL register and the contents of the A register and store it in the A register
    /// </summary>
    /// Verified with BGB
    public void ORAtAddressHLWithA(ICpuRegistersService registers)
    {
        var toOR = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        ORByteWithA(toOR, registers);
    }

    /// <summary>
    /// Perform a bit wise AND with the next byte in memory and the contents of the A register and store it in the A register
    /// Flags Z 0 H:1 0
    /// </summary>
    /// Verified against BGB
    public void ANDD8WithA(ICpuRegistersService registers)
    {
        var toAnd = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        //Since it's a D8 we need to add one extra to the PC and pump the clock once more than normal
        registers.ProgramCounter += 1;
        _clockService.Clock();
        ANDByteWithA(toAnd, registers);
    }

    /// <summary>
    /// Perform a bit wise XOR with the next byte in memory and the contents of the A register and store it in the A register
    /// Flags Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORD8WithA(ICpuRegistersService registers)
    {
        var toXor = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        //Since it's a D8 we need to add one extra to the PC and pump the clock once more than normal
        registers.ProgramCounter += 1;
        _clockService.Clock();
        XORByteWithA(toXor, registers);
    }

    private void ORByteWithA(byte toOr, ICpuRegistersService registers)
    {
        registers.A = (byte)(toOr | registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    private void ANDByteWithA(byte toAnd, ICpuRegistersService registers)
    {
        registers.A = (byte)(toAnd & registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = true;
        registers.F.Carry = false;
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    private void XORByteWithA(byte toXOR, ICpuRegistersService registers)
    {
        registers.A = (byte)(toXOR ^ registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}