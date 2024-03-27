using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public class ShiftRightInstructions : IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public ShiftRightInstructions(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x38, SRLB },
            { 0x39, SRLC },
            { 0x3A, SRLD },
            { 0x3B, SRLE },
            { 0x3C, SRLH },
            { 0x3D, SRLL },
            { 0x3E, SRLAtAddressHL },
            { 0x3F, SRLA },
        };
    }

    public void SRLB(ICpuRegistersService registers)
    {
        registers.B = SRLByte(registers.B, registers);
    }

    public void SRLC(ICpuRegistersService registers)
    {
        registers.C = SRLByte(registers.C, registers);
    }

    public void SRLD(ICpuRegistersService registers)
    {
        registers.D = SRLByte(registers.D, registers);
    }

    public void SRLE(ICpuRegistersService registers)
    {
        registers.E = SRLByte(registers.E, registers);
    }

    public void SRLH(ICpuRegistersService registers)
    {
        registers.H = SRLByte(registers.H, registers);
    }

    public void SRLL(ICpuRegistersService registers)
    {
        registers.L = SRLByte(registers.L, registers);
    }

    public void SRLAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        var srled = SRLByte(toSRL, registers);
        _mmuService.WriteByte(registers.HL, srled);
        _clockService.Clock();
    }
    
    public void SRLA(ICpuRegistersService registers)
    {
        registers.A = SRLByte(registers.A, registers);
    }

    private byte SRLByte(byte toSRL, ICpuRegistersService registers)
    {
        registers.F.Carry = (toSRL & 0x01) == 0x01;
        toSRL = (byte)(toSRL >> 1);
        registers.F.Zero = toSRL == 0;
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
        return toSRL;
    }
}