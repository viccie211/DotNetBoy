using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateRightThroughCarryInstructions : RotateInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateRightThroughCarryInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x18, RotateThroughCarryB },
            { 0x19, RotateThroughCarryC },
            { 0x1A, RotateThroughCarryD },
            { 0x1B, RotateThroughCarryE },
            { 0x1C, RotateThroughCarryH },
            { 0x1D, RotateThroughCarryL },
            { 0x1E, RotateThroughCarryAtAddresssHL },
            { 0x1F, RotateThroughCarryA },
        };
    }

    public void RotateThroughCarryB(ICpuRegistersService registers)
    {
        registers.B = RotateRightThroughCarry(registers.B, registers);
    }

    public void RotateThroughCarryC(ICpuRegistersService registers)
    {
        registers.C = RotateRightThroughCarry(registers.C, registers);
    }

    public void RotateThroughCarryD(ICpuRegistersService registers)
    {
        registers.D = RotateRightThroughCarry(registers.D, registers);
    }

    public void RotateThroughCarryE(ICpuRegistersService registers)
    {
        registers.E = RotateRightThroughCarry(registers.E, registers);
    }

    public void RotateThroughCarryH(ICpuRegistersService registers)
    {
        registers.H = RotateRightThroughCarry(registers.H, registers);
    }

    public void RotateThroughCarryL(ICpuRegistersService registers)
    {
        registers.L = RotateRightThroughCarry(registers.L, registers);
    }

    public void RotateThroughCarryAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateRightThroughCarry(toRotate, registers);
        _mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateThroughCarryA(ICpuRegistersService registers)
    {
        registers.A = RotateRightThroughCarry(registers.A, registers);
    }

    private byte RotateRightThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteRightThroughCarry(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
}