using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class JumpInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    private readonly IByteUshortService _byteUshortService;
    private readonly IClockService _clockService;

    public JumpInstructions(IMmuService mmuService, IClockService clockService, IByteUshortService byteUshortService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC3, Jump },
            { 0x18, JumpRelative8Bits },
            { 0x20, JumpRelative8BitsIfNotZero },
            { 0x28, JumpRelative8BitsIfZero },
            { 0xC9, Return },
            { 0xCD, CallA16 }
        };
        _mmuService = mmuService;
        _clockService = clockService;
        _byteUshortService = byteUshortService;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; init; }

    /// <summary>
    /// Jump to the address written next in memory.
    /// </summary>
    /// Verified against BGB
    public void Jump(ICpuRegistersService registers)
    {
        registers.ProgramCounter = _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock(4);
    }

    /// <summary>
    /// If the zero flag is set jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8BitsIfNotZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsIfTrue(!registers.F.Zero, registers);
    }

    /// <summary>
    /// If the zero flag is not set jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8BitsIfZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsIfTrue(registers.F.Zero, registers);
    }

    /// <summary>
    /// Call the subroutine on the address next in memory. It pushes the return address to the stack and then jumps to the new address
    /// </summary>
    /// Verified against BGB
    public void CallA16(ICpuRegistersService registers)
    {
        var toStore = (ushort)(registers.ProgramCounter + 3);
        var lower = _byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = _byteUshortService.UpperByteOfSixteenBits(toStore);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter = _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock(6);
    }

    /// <summary>
    /// Returns from a subroutine. It pops the return address from the stack and then jumps to that address
    /// </summary>
    /// Verified against BGB
    public void Return(ICpuRegistersService registers)
    {
        var lower = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        var upper = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        registers.ProgramCounter = _byteUshortService.CombineBytes(upper, lower);
        _clockService.Clock(4);
    }

    /// <summary>
    /// Jump relative according to the next (signed) byte in memory
    /// </summary>
    /// Verified against BGB
    public void JumpRelative8Bits(ICpuRegistersService registers)
    {
        JumpRelative(registers);
    }

    private void JumpRelative8BitsIfTrue(bool shouldJump, ICpuRegistersService registers)
    {
        if (shouldJump)
        {
            JumpRelative(registers);
            return;
        }

        registers.ProgramCounter += 2;
        _clockService.Clock(2);
    }

    private void JumpRelative(ICpuRegistersService registers)
    {
        var relative = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        registers.ProgramCounter += 2;
        registers.ProgramCounter = InstructionUtilFunctions.SignedAdd(registers.ProgramCounter, relative);
        _clockService.Clock(3);
    }
}