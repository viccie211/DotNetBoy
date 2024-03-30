using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateLeftThroughCarryInstructions : RotateInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateLeftThroughCarryInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x10, RotateThroughCarryB },
            { 0x11, RotateThroughCarryC },
            { 0x12, RotateThroughCarryD },
            { 0x13, RotateThroughCarryE },
            { 0x14, RotateThroughCarryH },
            { 0x15, RotateThroughCarryL },
            { 0x16, RotateThroughCarryAtAddresssHL },
            { 0x17, RotateThroughCarryA },
        };
    }

    public void RotateThroughCarryB(ICpuRegistersService registers)
    {
        registers.B = RotateLeftThroughCarry(registers.B, registers);
    }

    public void RotateThroughCarryC(ICpuRegistersService registers)
    {
        registers.C = RotateLeftThroughCarry(registers.C, registers);
    }

    public void RotateThroughCarryD(ICpuRegistersService registers)
    {
        registers.D = RotateLeftThroughCarry(registers.D, registers);
    }

    public void RotateThroughCarryE(ICpuRegistersService registers)
    {
        registers.E = RotateLeftThroughCarry(registers.E, registers);
    }

    public void RotateThroughCarryH(ICpuRegistersService registers)
    {
        registers.H = RotateLeftThroughCarry(registers.H, registers);
    }

    public void RotateThroughCarryL(ICpuRegistersService registers)
    {
        registers.L = RotateLeftThroughCarry(registers.L, registers);
    }

    public void RotateThroughCarryAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateLeftThroughCarry(toRotate, registers);
        _mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateThroughCarryA(ICpuRegistersService registers)
    {
        registers.A = RotateLeftThroughCarry(registers.A, registers);
    }

    private byte RotateLeftThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteLeftThroughCarry(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
}