using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftLeftArithmeticInstructions : ShiftInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    private readonly IMmuService _mmuService;

    public ShiftLeftArithmeticInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x20, ShiftLeftArithmeticB },
            { 0x21, ShiftLeftArithmeticC },
            { 0x22, ShiftLeftArithmeticD },
            { 0x23, ShiftLeftArithmeticE },
            { 0x24, ShiftLeftArithmeticH },
            { 0x25, ShiftLeftArithmeticL },
            { 0x26, ShiftLeftArithmeticAtAddressHL },
            { 0x27, ShiftLeftArithmeticA },
        };
    }

    public void ShiftLeftArithmeticB(ICpuRegistersService registers)
    {
        registers.B = ShiftLeftArithmeticByte(registers.B, registers);
    }

    public void ShiftLeftArithmeticC(ICpuRegistersService registers)
    {
        registers.C = ShiftLeftArithmeticByte(registers.C, registers);
    }

    public void ShiftLeftArithmeticD(ICpuRegistersService registers)
    {
        registers.D = ShiftLeftArithmeticByte(registers.D, registers);
    }

    public void ShiftLeftArithmeticE(ICpuRegistersService registers)
    {
        registers.E = ShiftLeftArithmeticByte(registers.E, registers);
    }

    public void ShiftLeftArithmeticH(ICpuRegistersService registers)
    {
        registers.H = ShiftLeftArithmeticByte(registers.H, registers);
    }

    public void ShiftLeftArithmeticL(ICpuRegistersService registers)
    {
        registers.L = ShiftLeftArithmeticByte(registers.L, registers);
    }

    public void ShiftLeftArithmeticAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftLeftArithmeticByte(toSRL, registers);
        _mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }

    public void ShiftLeftArithmeticA(ICpuRegistersService registers)
    {
        registers.A = ShiftLeftArithmeticByte(registers.A, registers);
    }
}