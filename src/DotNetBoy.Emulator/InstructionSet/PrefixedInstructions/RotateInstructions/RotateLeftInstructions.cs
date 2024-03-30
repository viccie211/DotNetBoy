using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateLeftInstructions : RotateInstructionsBase, IInstructionSet
{
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public RotateLeftInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x00, RotateB },
            { 0x01, RotateC },
            { 0x02, RotateD },
            { 0x03, RotateE },
            { 0x04, RotateH },
            { 0x05, RotateL },
            { 0x06, RotateAtAddresssHL },
            { 0x07, RotateA },
        };
    }

    public void RotateB(ICpuRegistersService registers)
    {
        registers.B = RotateLeft(registers.B, registers);
    }

    public void RotateC(ICpuRegistersService registers)
    {
        registers.C = RotateLeft(registers.C, registers);
    }

    public void RotateD(ICpuRegistersService registers)
    {
        registers.D = RotateLeft(registers.D, registers);
    }

    public void RotateE(ICpuRegistersService registers)
    {
        registers.E = RotateLeft(registers.E, registers);
    }

    public void RotateH(ICpuRegistersService registers)
    {
        registers.H = RotateLeft(registers.H, registers);
    }

    public void RotateL(ICpuRegistersService registers)
    {
        registers.L = RotateLeft(registers.L, registers);
    }

    public void RotateAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateLeft(toRotate, registers);
        _mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateA(ICpuRegistersService registers)
    {
        registers.A = RotateLeft(registers.A, registers);
    }

    private byte RotateLeft(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteLeft(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
}