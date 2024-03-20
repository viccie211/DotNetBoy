using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public class RotateRightInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateRightInstructions(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x18, RotateB },
            { 0x19, RotateC },
            { 0x1A, RotateD },
            { 0x1B, RotateE },
            { 0x1C, RotateH },
            { 0x1D, RotateL },
            { 0x1E, RotateAtAddresssHL },
            { 0x1F, RotateA },
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
        _clockService.Clock();
        var rotated = RotateRight(toRotate, registers);
        _mmuService.WriteByte(registers.HL, rotated);
        _clockService.Clock();
    }

    public void RotateA(ICpuRegistersService registers)
    {
        registers.A = RotateRight(registers.A, registers);
    }

    private byte RotateRight(byte toRotate, ICpuRegistersService registers)
    {
        var newCarry = (toRotate & 0x01) == 0x01;
        var shifted = (byte)(toRotate >> 1);

        if (registers.F.Carry)
        {
            shifted += 0x80;
        }

        registers.F.Carry = newCarry;
        registers.F.Zero = shifted == 0;
        registers.ProgramCounter += 2;
        _clockService.Clock(2);
        return shifted;
    }
}