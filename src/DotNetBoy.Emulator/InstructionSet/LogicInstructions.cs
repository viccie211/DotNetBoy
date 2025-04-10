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
            { 0xA0, ANDBWithA },
            { 0xA1, ANDCWithA },
            { 0xA2, ANDDWithA },
            { 0xA3, ANDEWithA },
            { 0xA4, ANDHWithA },
            { 0xA5, ANDLWithA },
            { 0xA6, ANDAtAddressHLWithA },
            { 0xA7, ANDAWithA },
            { 0xA8, XORBWithA },
            { 0xA9, XORCWithA },
            { 0xAA, XORDWithA },
            { 0xAB, XOREWithA },
            { 0xAC, XORHWithA },
            { 0xAD, XORLWithA },
            { 0xAE, XORAtAddressHLWithA },
            { 0xAF, XORAWithA },
            { 0xB0, ORBWithA },
            { 0xB1, ORCWithA },
            { 0xB2, ORDWithA },
            { 0xB3, OREWithA },
            { 0xB4, ORHWithA },
            { 0xB5, ORLWithA },
            { 0xB6, ORAtAddressHLWithA },
            { 0xB7, ORAWithA },
            { 0xB8, CompareBWithA },
            { 0xB9, CompareCWithA },
            { 0xBA, CompareDWithA },
            { 0xBB, CompareEWithA },
            { 0xBC, CompareHWithA },
            { 0xBD, CompareLWithA },
            { 0xBE, CompareAtAddressHLWithA },
            { 0xBF, CompareAWithA },
            { 0xE6, ANDD8WithA },
            { 0xEE, XORD8WithA },
            { 0xF6, ORD8WithA },
            { 0xFE, CompareD8WithA },
        };
        _clockService = clockService;
    }


    #region AND

    public void ANDBWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.B, registers);
    }

    public void ANDCWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.C, registers);
    }

    public void ANDDWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.D, registers);
    }

    public void ANDEWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.E, registers);
    }

    public void ANDHWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.H, registers);
    }

    public void ANDLWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.L, registers);
    }

    public void ANDAtAddressHLWithA(ICpuRegistersService registers)
    {
        var toAND = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        ANDByteWithA(toAND, registers);
    }

    public void ANDAWithA(ICpuRegistersService registers)
    {
        ANDByteWithA(registers.A, registers);
    }

    #endregion

    #region XOR

    /// <summary>
    /// Perform a bit wise XOR with the contents of the B register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORBWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.B, registers);
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

    /// <summary>
    /// Perform a bit wise XOR with the contents of the D register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORDWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.D, registers);
    }

    /// <summary>
    /// Perform a bit wise XOR with the contents of the E register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XOREWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.E, registers);
    }

    /// <summary>
    /// Perform a bit wise XOR with the contents of the H register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORHWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.H, registers);
    }

    /// <summary>
    /// Perform a bit wise XOR with the contents of the L register and the contents of the A register and store it in the A register
    /// Sets Z 0 0 0
    /// </summary>
    /// Verified against BGB
    public void XORLWithA(ICpuRegistersService registers)
    {
        XORByteWithA(registers.L, registers);
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

    #endregion

    #region OR

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the B register and the contents of the A register and store it in the A register
    /// </summary>
    /// 
    public void ORBWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.B, registers);
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
    /// Perform a bit wise OR operation with the contents of the D register and the contents of the A register and store it in the A register
    /// </summary>
    /// 
    public void ORDWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.D, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the E register and the contents of the A register and store it in the A register
    /// </summary>
    /// 
    public void OREWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.E, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the H register and the contents of the A register and store it in the A register
    /// </summary>
    /// 
    public void ORHWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.H, registers);
    }

    /// <summary>
    /// Perform a bit wise OR operation with the contents of the L register and the contents of the A register and store it in the A register
    /// </summary>
    /// 
    public void ORLWithA(ICpuRegistersService registers)
    {
        ORByteWithA(registers.L, registers);
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
    /// Perform a bit wise OR operation with the contents of memory at the address specified by the HL register and the contents of the A register and store it in the A register
    /// </summary>
    /// Verified with BGB
    public void ORAtAddressHLWithA(ICpuRegistersService registers)
    {
        var toOR = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        ORByteWithA(toOR, registers);
    }

    public void ORD8WithA(ICpuRegistersService registers)
    {
        var toOR = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter++;
        ORByteWithA(toOR, registers);
    }

    #endregion

    #region Compare

    public void CompareBWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.B, registers);
    }

    public void CompareCWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.C, registers);
    }

    public void CompareDWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.D, registers);
    }

    public void CompareEWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.E, registers);
    }

    public void CompareHWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.H, registers);
    }

    public void CompareLWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.L, registers);
    }

    public void CompareAtAddressHLWithA(ICpuRegistersService registers)
    {
        var toCompare = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        CompareByteWithA(toCompare, registers);
    }

    public void CompareAWithA(ICpuRegistersService registers)
    {
        CompareByteWithA(registers.A, registers);
    }

    public void CompareD8WithA(ICpuRegistersService registers)
    {
        var d8 = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));

        _clockService.Clock();
        registers.ProgramCounter += 1;
        CompareByteWithA(d8, registers);
    }

    #endregion

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

    private void ORByteWithA(byte toOr, ICpuRegistersService registers)
    {
        registers.A = (byte)(toOr | registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
    }

    private void ANDByteWithA(byte toAnd, ICpuRegistersService registers)
    {
        registers.A = (byte)(toAnd & registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = true;
        registers.F.Carry = false;
        registers.ProgramCounter += 1;
    }

    private void XORByteWithA(byte toXOR, ICpuRegistersService registers)
    {
        registers.A = (byte)(toXOR ^ registers.A);
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.F.Carry = false;
        registers.F.HalfCarry = false;
        registers.ProgramCounter += 1;
    }

    private void CompareByteWithA(byte toCompare, ICpuRegistersService registers)
    {
        var result = (byte)(registers.A - toCompare);
        registers.F.Subtract = true;
        registers.F.Zero = result == 0;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(registers.A, toCompare);
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtraction(registers.A, toCompare);
        registers.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
}