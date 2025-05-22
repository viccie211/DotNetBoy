using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class JumpInstructions(IMmuService mmuService, IClockService clockService, IByteUshortService byteUshortService) : IInstructionSet
{
    #region Jumps

    /// <summary>
    /// Jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8Bits(ICpuRegistersService registers)
    {
        JumpRelative(registers);
    }

    /// <summary>
    /// If the zero flag is not set jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8BitsIfNotZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsOnCondition(!registers.F.Zero, registers);
    }

    /// <summary>
    /// If the zero flag is set jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8BitsIfZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsOnCondition(registers.F.Zero, registers);
    }

    /// <summary>
    /// If the carry flag is set jump relative according to the next (signed) byte in memory
    /// </summary>
    public void JumpRelative8BitsIfNotCarry(ICpuRegistersService registers)
    {
        JumpRelative8BitsOnCondition(!registers.F.Carry, registers);
    }

    /// <summary>
    /// If the carry flag is set jump relative according to the next (signed) byte in memory
    /// </summary>
    public void JumpRelative8BitsIfCarry(ICpuRegistersService registers)
    {
        JumpRelative8BitsOnCondition(registers.F.Carry, registers);
    }

    /// <summary>
    /// Jump to the address written next in memory.
    /// </summary>
    /// Verified against BGB
    public void JumpA16(ICpuRegistersService registers)
    {
        JumpToA16(registers);
    }

    public void JumpA16OnZero(ICpuRegistersService registers)
    {
        JumpA16OnCondition(registers.F.Zero, registers);
    }

    public void JumpA16OnNonZero(ICpuRegistersService registers)
    {
        JumpA16OnCondition(!registers.F.Zero, registers);
    }

    public void JumpA16OnCarry(ICpuRegistersService registers)
    {
        JumpA16OnCondition(registers.F.Carry, registers);
    }

    public void JumpA16OnNonCarry(ICpuRegistersService registers)
    {
        JumpA16OnCondition(!registers.F.Carry, registers);
    }

    /// <summary>
    /// Jump to the address specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void JumpToAddressHL(ICpuRegistersService registers)
    {
        registers.ProgramCounter = registers.HL;
    }

    #endregion

    #region Calls

    //TODO: Write unit tests
    public void CallA16Zero(ICpuRegistersService registers)
    {
        CallA16OnCondition(registers.F.Zero, registers);
    }

    public void CallA16NonZero(ICpuRegistersService registers)
    {
        CallA16OnCondition(!registers.F.Zero, registers);
    }

    //TODO: Write unit tests
    public void CallA16Carry(ICpuRegistersService registers)
    {
        CallA16OnCondition(registers.F.Carry, registers);
    }

    //TODO: Write unit tests
    public void CallA16NonCarry(ICpuRegistersService registers)
    {
        CallA16OnCondition(!registers.F.Carry, registers);
    }

    /// <summary>
    /// Call the subroutine on the address next in memory. It pushes the return address to the stack and then jumps to the new address
    /// </summary>
    /// Verified against BGB
    public void CallA16(ICpuRegistersService registers)
    {
        Call(registers);
    }

    #endregion

    #region Returns

    public void ReturnNonZero(ICpuRegistersService registers)
    {
        ReturnOnCondition(!registers.F.Zero, registers);
    }

    public void ReturnZero(ICpuRegistersService registers)
    {
        ReturnOnCondition(registers.F.Zero, registers);
    }

    public void ReturnNonCarry(ICpuRegistersService registers)
    {
        ReturnOnCondition(!registers.F.Carry, registers);
    }

    public void ReturnCarry(ICpuRegistersService registers)
    {
        ReturnOnCondition(registers.F.Carry, registers);
    }

    /// <summary>
    /// Returns from a subroutine. It pops the return address from the stack and then jumps to that address
    /// </summary>
    /// Verified against BGB
    public void ReturnFromSubroutine(ICpuRegistersService registers)
    {
        Return(registers);
    }

    /// <summary>
    /// Returns from a subroutine. It pops the return address from the stack and then jumps to that address
    /// </summary>
    /// Verified against BGB
    public void ReturnFromInterrupt(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = true;
        registers.InterruptsJustEnabled = true;
        Return(registers);
        clockService.Clock();
    }

    #endregion

    #region Resets

    public void Reset0(ICpuRegistersService registers)
    {
        Reset(0x0000, registers);
    }

    public void Reset1(ICpuRegistersService registers)
    {
        Reset(0x0008, registers);
    }

    public void Reset2(ICpuRegistersService registers)
    {
        Reset(0x0010, registers);
    }

    public void Reset3(ICpuRegistersService registers)
    {
        Reset(0x0018, registers);
    }

    public void Reset4(ICpuRegistersService registers)
    {
        Reset(0x0020, registers);
    }

    public void Reset5(ICpuRegistersService registers)
    {
        Reset(0x0028, registers);
    }

    public void Reset6(ICpuRegistersService registers)
    {
        Reset(0x0030, registers);
    }

    public void Reset7(ICpuRegistersService registers)
    {
        Reset(0x0038, registers);
    }

    #endregion

    #region private methods

    private void JumpRelative8BitsOnCondition(bool shouldJump, ICpuRegistersService registers)
    {
        if (shouldJump)
        {
            JumpRelative(registers);
            return;
        }

        registers.ProgramCounter += 2;
        clockService.Clock();
    }

    private void JumpRelative(ICpuRegistersService registers)
    {
        var relative = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        registers.ProgramCounter += 2;
        registers.ProgramCounter = InstructionUtilFunctions.SignedAdd(registers.ProgramCounter, relative);
        clockService.Clock(2);
    }

    private void JumpA16OnCondition(bool shouldJump, ICpuRegistersService registers)
    {
        if (shouldJump)
        {
            JumpToA16(registers);
            return;
        }

        registers.ProgramCounter += 3;
        clockService.Clock(2);
    }

    private void JumpToA16(ICpuRegistersService registers)
    {
        var address = mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        registers.ProgramCounter = address;
        clockService.Clock(3);
    }

    private void CallA16OnCondition(bool shouldCall, ICpuRegistersService registers)
    {
        if (shouldCall)
        {
            Call(registers);
            return;
        }

        registers.ProgramCounter += 3;
        clockService.Clock(2);
    }

    private void Call(ICpuRegistersService registers)
    {
        var toStore = (ushort)(registers.ProgramCounter + 3);
        var lower = byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = byteUshortService.UpperByteOfSixteenBits(toStore);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter =
            mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock(5);
    }

    private void Reset(ushort address, ICpuRegistersService registers)
    {
        var toStore = (ushort)(registers.ProgramCounter + 1);
        var lower = byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = byteUshortService.UpperByteOfSixteenBits(toStore);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter = address;
        clockService.Clock(3);
    }

    private void ReturnOnCondition(bool condition, ICpuRegistersService registers)
    {
        if (condition)
        {
            clockService.Clock();
            Return(registers);
            return;
        }

        clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void Return(ICpuRegistersService registers)
    {
        var lower = mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        var upper = mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        registers.ProgramCounter = byteUshortService.CombineBytes(upper, lower);
        clockService.Clock(3);
    }

    #endregion

    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x18:
                JumpRelative8Bits(registers);
                break;
            case 0x20:
                JumpRelative8BitsIfNotZero(registers);
                break;
            case 0x28:
                JumpRelative8BitsIfZero(registers);
                break;
            case 0x30:
                JumpRelative8BitsIfNotCarry(registers);
                break;
            case 0x38:
                JumpRelative8BitsIfCarry(registers);
                break;

            case 0xC0:
                ReturnNonZero(registers);
                break;
            case 0xC2:
                JumpA16OnNonZero(registers);
                break;
            case 0xC3:
                JumpA16(registers);
                break;
            case 0xC4:
                CallA16NonZero(registers);
                break;
            case 0xC7:
                Reset0(registers);
                break;
            case 0xC8:
                ReturnZero(registers);
                break;
            case 0xC9:
                ReturnFromSubroutine(registers);
                break;
            case 0xCA:
                JumpA16OnZero(registers);
                break;
            case 0xCC:
                CallA16Zero(registers);
                break;
            case 0xCD:
                CallA16(registers);
                break;
            case 0xCF:
                Reset1(registers);
                break;
            case 0xD0:
                ReturnNonCarry(registers);
                break;
            case 0xD2:
                JumpA16OnNonCarry(registers);
                break;
            case 0xD4:
                CallA16NonCarry(registers);
                break;
            case 0xD7:
                Reset2(registers);
                break;
            case 0xD8:
                ReturnCarry(registers);
                break;
            case 0xD9:
                ReturnFromInterrupt(registers);
                break;
            case 0xDA:
                JumpA16OnCarry(registers);
                break;
            case 0xDC:
                CallA16Carry(registers);
                break;
            case 0xDF:
                Reset3(registers);
                break;
            case 0xE7:
                Reset4(registers);
                break;
            case 0xE9:
                JumpToAddressHL(registers);
                break;
            case 0xEF:
                Reset5(registers);
                break;
            case 0xF7:
                Reset6(registers);
                break;
            case 0xFF:
                Reset7(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented.");
        }
    }
}