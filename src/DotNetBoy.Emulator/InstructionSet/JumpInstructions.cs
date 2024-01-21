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
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0xC3, Jump },
            { 0x20, JumpRelative8BitsIfNotZero },
            { 0xCD, CallA16 }
        };
        _mmuService = mmuService;
        _clockService = clockService;
        _byteUshortService = byteUshortService;
    }

    private void Jump(CpuRegisters cpu)
    {
        cpu.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _clockService.Clock(4);
    }

    private void JumpRelative8BitsIfNotZero(CpuRegisters cpu)
    {
        if (!cpu.F.Zero)
        {
            cpu.ProgramCounter = InstructionUtilFunctions.SignedAdd(cpu.ProgramCounter, _mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1)));
            _clockService.Clock(3);
            return;
        }

        cpu.ProgramCounter += 2;
        _clockService.Clock(2);
    }

    private void CallA16(CpuRegisters cpu)
    {
        var toStore = (ushort)(cpu.ProgramCounter+3);
        var lower = _byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = _byteUshortService.UpperByteOfSixteenBits(toStore);
        cpu.StackPointer--;
        _mmuService.WriteByte(cpu.StackPointer,upper);
        cpu.StackPointer--;
        _mmuService.WriteByte(cpu.StackPointer,lower);
        cpu.ProgramCounter = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter+1));
        _clockService.Clock(6);
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; init; }
}