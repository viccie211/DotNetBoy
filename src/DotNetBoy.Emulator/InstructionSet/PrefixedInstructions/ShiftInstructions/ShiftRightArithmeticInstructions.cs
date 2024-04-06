using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftRightArithmeticInstructions : ShiftInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    private readonly IMmuService _mmuService;

    public ShiftRightArithmeticInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x28, ShiftRightArithmeticB },
            { 0x29, ShiftRightArithmeticC },
            { 0x2A, ShiftRightArithmeticD },
            { 0x2B, ShiftRightArithmeticE },
            { 0x2C, ShiftRightArithmeticH },
            { 0x2D, ShiftRightArithmeticL },
            { 0x2E, ShiftRightArithmeticAtAddressHL },
            { 0x2F, ShiftRightArithmeticA },
        };
    }

    public void ShiftRightArithmeticB(ICpuRegistersService registers)
    {
        registers.B = ShiftRightArithmeticByte(registers.B, registers);
    }

    public void ShiftRightArithmeticC(ICpuRegistersService registers)
    {
        registers.C = ShiftRightArithmeticByte(registers.C, registers);
    }

    public void ShiftRightArithmeticD(ICpuRegistersService registers)
    {
        registers.D = ShiftRightArithmeticByte(registers.D, registers);
    }

    public void ShiftRightArithmeticE(ICpuRegistersService registers)
    {
        registers.E = ShiftRightArithmeticByte(registers.E, registers);
    }

    public void ShiftRightArithmeticH(ICpuRegistersService registers)
    {
        registers.H = ShiftRightArithmeticByte(registers.H, registers);
    }

    public void ShiftRightArithmeticL(ICpuRegistersService registers)
    {
        registers.L = ShiftRightArithmeticByte(registers.L, registers);
    }

    public void ShiftRightArithmeticAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftRightArithmeticByte(toSRL, registers);
        _mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }

    public void ShiftRightArithmeticA(ICpuRegistersService registers)
    {
        registers.A = ShiftRightArithmeticByte(registers.A, registers);
    }
}