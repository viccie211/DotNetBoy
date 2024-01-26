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

    public void Jump(ICpuRegistersService registers)
    {
        registers.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(registers.ProgramCounter + 1));
        _clockService.Clock(4);
    }

    public void JumpRelative8BitsIfNotZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsIfTrue(!registers.F.Zero, registers);
    }

    public void JumpRelative8BitsIfZero(ICpuRegistersService registers)
    {
        JumpRelative8BitsIfTrue(registers.F.Zero, registers);
    }

    public void CallA16(ICpuRegistersService registers)
    {
        var toStore = (ushort)(registers.ProgramCounter + 3);
        var lower = _byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = _byteUshortService.UpperByteOfSixteenBits(toStore);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(registers.ProgramCounter + 1));
        _clockService.Clock(6);
    }

    public void Return(ICpuRegistersService registers)
    {
        var lower = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        var upper = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        registers.ProgramCounter = _byteUshortService.CombineBytes(upper, lower);
        _clockService.Clock(4);
    }

    private void JumpRelative8BitsIfTrue(bool shouldJump, ICpuRegistersService registers)
    {
        if (shouldJump)
        {
            registers.ProgramCounter = InstructionUtilFunctions.SignedAdd(registers.ProgramCounter, _mmuService.ReadByte((ushort)(registers.ProgramCounter + 1)));
            _clockService.Clock(3);
            return;
        }

        registers.ProgramCounter += 2;
        _clockService.Clock(2);
    }
}