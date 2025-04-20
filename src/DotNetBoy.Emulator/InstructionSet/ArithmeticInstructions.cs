using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class ArithmeticInstructions(IClockService clockService, IMmuService mmuService) : IInstructionSet
{
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
        var toAdd = mmuService.ReadByte(registers.HL);
        clockService.Clock();
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
        var toAdd = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock();
        registers.ProgramCounter += 1;
        AddByteToA(toAdd, registers);
    }

    #endregion

    #region AddWithCarry

    public void AddBWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.B, registers);
    }

    public void AddCWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.C, registers);
    }

    public void AddDWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.D, registers);
    }

    public void AddEWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.E, registers);
    }

    public void AddHWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.H, registers);
    }

    public void AddLWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.L, registers);
    }

    public void AddAtAddressHLWithCarryToA(ICpuRegistersService registers)
    {
        var toAdd = mmuService.ReadByte(registers.HL);
        clockService.Clock();
        AddByteWithCarryToA(toAdd, registers);
    }

    public void AddAWithCarryToA(ICpuRegistersService registers)
    {
        AddByteWithCarryToA(registers.A, registers);
    }

    public void AddD8WithCarryToA(ICpuRegistersService registers)
    {
        var toAdd = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock();
        registers.ProgramCounter += 1;
        AddByteWithCarryToA(toAdd, registers);
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
        SubtractByteFromA(registers.D, registers);
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
        var toSubtract = mmuService.ReadByte(registers.HL);
        clockService.Clock();
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
        var toSubtract = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock();
        registers.ProgramCounter += 1;
        SubtractByteFromA(toSubtract, registers);
    }

    #endregion

    #region SubtractWithCarry

    public void SubtractBWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.B, registers);
    }

    public void SubtractCWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.C, registers);
    }

    public void SubtractDWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.D, registers);
    }

    public void SubtractEWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.E, registers);
    }

    public void SubtractHWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.H, registers);
    }

    public void SubtractLWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.L, registers);
    }

    public void SubtractAtAddressHLWithCarryFromA(ICpuRegistersService registers)
    {
        var toSubtract = mmuService.ReadByte(registers.HL);
        clockService.Clock();
        SubtractByteWithCarryFromA(toSubtract, registers);
    }

    public void SubtractAWithCarryFromA(ICpuRegistersService registers)
    {
        SubtractByteWithCarryFromA(registers.A, registers);
    }

    /// <summary>
    /// Subtracts the next byte in memory from the A register and stores the result in the A register
    /// Sets Z 1 H C
    /// </summary>
    /// 
    public void SubtractD8WithCarryFromA(ICpuRegistersService registers)
    {
        var toSubtract = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock();
        registers.ProgramCounter += 1;
        SubtractByteWithCarryFromA(toSubtract, registers);
    }

    #endregion


    #region SixteenBitAdditions

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

    #endregion

    public void AddSignedByteToStackPointer(ICpuRegistersService registers)
    {
        var toAddUnsigned = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        var signedToAdd = InstructionUtilFunctions.ByteToSignedByte(toAddUnsigned);
        var resultInt = registers.StackPointer + signedToAdd;
        var result = (ushort)resultInt;
        registers.F.Zero = false;
        registers.F.Subtract = false;
        registers.F.HalfCarry = (result & 0x0f) < (registers.StackPointer & 0x0f);
        registers.F.Carry = (result & 0xff) < (registers.StackPointer & 0xff);
        registers.StackPointer = result;
        registers.ProgramCounter += 2;
        clockService.Clock(3);
    }


    #region Private methods

    private void AddByteWithCarryToA(byte toAdd, ICpuRegistersService registers)
    {
        var carryBit = registers.F.Carry ? 1 : 0;
        var result = registers.A + toAdd + carryBit;
        registers.F.Zero = (byte)result == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = (result & 0x0F) < (registers.A & 0x0F) + carryBit;
        registers.F.Carry = result > 255;
        registers.A = (byte)result;
        registers.ProgramCounter += 1;
    }

    private void AddByteToA(byte toAdd, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitAddition(toAdd, registers.A);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitAddition(toAdd, registers.A);
        registers.A += toAdd;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = false;
        registers.ProgramCounter += 1;
    }

    private void SubtractByteWithCarryFromA(byte toSubtract, ICpuRegistersService registers)
    {
        byte carryBit = (byte)(registers.F.Carry ? 1 : 0);
        var result = registers.A - toSubtract - carryBit;
        registers.F.Zero = (byte)result == 0;
        registers.F.Subtract = true;
        registers.F.HalfCarry = ((registers.A & 0x0f) - carryBit) < (toSubtract & 0x0f);
        registers.F.Carry = result < 0;
        registers.ProgramCounter += 1;
        registers.A = (byte)result;
    }

    private void SubtractByteFromA(byte toSubtract, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtraction(registers.A, toSubtract);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(registers.A, toSubtract);
        registers.A -= toSubtract;
        registers.F.Zero = registers.A == 0;
        registers.F.Subtract = true;
        registers.ProgramCounter += 1;
    }

    public ushort SixteenBitAddition(ushort a, ushort b, ICpuRegistersService registers)
    {
        registers.F.Carry = InstructionUtilFunctions.CarryFor16BitAddition(a, b);
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor16BitAddition(a, b);
        registers.F.Subtract = false;
        var result = (ushort)(a + b);
        clockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }

    #endregion

    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x09:
                AddBCToHL(registers);
                break;
            case 0x19:
                AddDEToHL(registers);
                break;
            case 0x29:
                AddHLToHL(registers);
                break;
            case 0x39:
                AddStackPointerToHL(registers);
                break;
            case 0x80:
                AddBToA(registers);
                break;
            case 0x81:
                AddCToA(registers);
                break;
            case 0x82:
                AddDToA(registers);
                break;
            case 0x83:
                AddEToA(registers);
                break;
            case 0x84:
                AddHToA(registers);
                break;
            case 0x85:
                AddLToA(registers);
                break;
            case 0x86:
                AddAtAddressHLToA(registers);
                break;
            case 0x87:
                AddAToA(registers);
                break;
            case 0x88:
                AddBWithCarryToA(registers);
                break;
            case 0x89:
                AddCWithCarryToA(registers);
                break;
            case 0x8A:
                AddDWithCarryToA(registers);
                break;
            case 0x8B:
                AddEWithCarryToA(registers);
                break;
            case 0x8C:
                AddHWithCarryToA(registers);
                break;
            case 0x8D:
                AddLWithCarryToA(registers);
                break;
            case 0x8E:
                AddAtAddressHLWithCarryToA(registers);
                break;
            case 0x8F:
                AddAWithCarryToA(registers);
                break;
            case 0x90:
                SubtractBFromA(registers);
                break;
            case 0x91:
                SubtractCFromA(registers);
                break;
            case 0x92:
                SubtractDFromA(registers);
                break;
            case 0x93:
                SubtractEFromA(registers);
                break;
            case 0x94:
                SubtractHFromA(registers);
                break;
            case 0x95:
                SubtractLFromA(registers);
                break;
            case 0x96:
                SubtractAtAddressHLFromA(registers);
                break;
            case 0x97:
                SubtractAFromA(registers);
                break;
            case 0x98:
                SubtractBWithCarryFromA(registers);
                break;
            case 0x99:
                SubtractCWithCarryFromA(registers);
                break;
            case 0x9A:
                SubtractDWithCarryFromA(registers);
                break;
            case 0x9B:
                SubtractEWithCarryFromA(registers);
                break;
            case 0x9C:
                SubtractHWithCarryFromA(registers);
                break;
            case 0x9D:
                SubtractLWithCarryFromA(registers);
                break;
            case 0x9E:
                SubtractAtAddressHLWithCarryFromA(registers);
                break;
            case 0x9F:
                SubtractAWithCarryFromA(registers);
                break;
            case 0xC6:
                AddD8ToA(registers);
                break;
            case 0xCE:
                AddD8WithCarryToA(registers);
                break;
            case 0xD6:
                SubtractD8FromA(registers);
                break;
            case 0xDE:
                SubtractD8WithCarryFromA(registers);
                break;
            case 0xE8:
                AddSignedByteToStackPointer(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented.");
        }
    }
}