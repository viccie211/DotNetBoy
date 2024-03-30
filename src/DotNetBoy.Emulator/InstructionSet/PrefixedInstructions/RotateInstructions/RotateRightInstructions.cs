using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateRightInstructions : RotateInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateRightInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x08, RotateB },
            { 0x09, RotateC },
            { 0x0A, RotateD },
            { 0x0B, RotateE },
            { 0x0C, RotateH },
            { 0x0D, RotateL },
            { 0x0E, RotateAtAddresssHL },
            { 0x0F, RotateA },
        };
    }

    public void RotateB(ICpuRegistersService registers)
    {
        registers.B = RotateRight(registers.B, registers);
    }

    public void RotateC(ICpuRegistersService registers)
    {
        registers.C = RotateRight(registers.C, registers);
    }

    public void RotateD(ICpuRegistersService registers)
    {
        registers.D = RotateRight(registers.D, registers);
    }

    public void RotateE(ICpuRegistersService registers)
    {
        registers.E = RotateRight(registers.E, registers);
    }

    public void RotateH(ICpuRegistersService registers)
    {
        registers.H = RotateRight(registers.H, registers);
    }

    public void RotateL(ICpuRegistersService registers)
    {
        registers.L = RotateRight(registers.L, registers);
    }

    public void RotateAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateRight(toRotate, registers);
        _mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateA(ICpuRegistersService registers)
    {
        registers.A = RotateRight(registers.A, registers);
    }

    private byte RotateRight(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteRight(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
}