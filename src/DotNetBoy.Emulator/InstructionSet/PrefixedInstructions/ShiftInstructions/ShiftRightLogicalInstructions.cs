using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftRightLogicalInstructions : ShiftInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
    
    private readonly IMmuService _mmuService;

    public ShiftRightLogicalInstructions(IClockService clockService, IMmuService mmuService) : base(clockService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x38, ShiftRightLogicalB },
            { 0x39, ShiftRightLogicalC },
            { 0x3A, ShiftRightLogicalD },
            { 0x3B, ShiftRightLogicalE },
            { 0x3C, ShiftRightLogicalH },
            { 0x3D, ShiftRightLogicalL },
            { 0x3E, ShiftRightLogicalAtAddressHL },
            { 0x3F, ShiftRightLogicalA },
        };
    }

    public void ShiftRightLogicalB(ICpuRegistersService registers)
    {
        registers.B = ShiftRightLogicalByte(registers.B, registers);
    }

    public void ShiftRightLogicalC(ICpuRegistersService registers)
    {
        registers.C = ShiftRightLogicalByte(registers.C, registers);
    }

    public void ShiftRightLogicalD(ICpuRegistersService registers)
    {
        registers.D = ShiftRightLogicalByte(registers.D, registers);
    }

    public void ShiftRightLogicalE(ICpuRegistersService registers)
    {
        registers.E = ShiftRightLogicalByte(registers.E, registers);
    }

    public void ShiftRightLogicalH(ICpuRegistersService registers)
    {
        registers.H = ShiftRightLogicalByte(registers.H, registers);
    }

    public void ShiftRightLogicalL(ICpuRegistersService registers)
    {
        registers.L = ShiftRightLogicalByte(registers.L, registers);
    }

    public void ShiftRightLogicalAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = _mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftRightLogicalByte(toSRL, registers);
        _mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }
    
    public void ShiftRightLogicalA(ICpuRegistersService registers)
    {
        registers.A = ShiftRightLogicalByte(registers.A, registers);
    }
}