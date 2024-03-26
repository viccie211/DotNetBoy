using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public class SwapInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SwapInstructions(IClockService clockService, IMmuService mmuService)
    {
        _clockService = clockService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x30, SwapB },
            { 0x31, SwapC },
            { 0x32, SwapD },
            { 0x33, SwapE },
            { 0x34, SwapH },
            { 0x35, SwapL },
            { 0x36, SwapAtAddressHL },
            { 0x37, SwapA },
        };
    }

    public void SwapB(ICpuRegistersService registers)
    {
        registers.B = SwapNibbles(registers.B, registers);
    }

    public void SwapC(ICpuRegistersService registers)
    {
        registers.C = SwapNibbles(registers.C, registers);
    }

    public void SwapD(ICpuRegistersService registers)
    {
        registers.D = SwapNibbles(registers.D, registers);
    }

    public void SwapE(ICpuRegistersService registers)
    {
        registers.E = SwapNibbles(registers.E, registers);
    }

    public void SwapH(ICpuRegistersService registers)
    {
        registers.H = SwapNibbles(registers.H, registers);
    }

    public void SwapL(ICpuRegistersService registers)
    {
        registers.L = SwapNibbles(registers.L, registers);
    }

    public void SwapAtAddressHL(ICpuRegistersService registers)
    {
        var toSwap = _mmuService.ReadByte(registers.HL);
        _clockService.Clock();
        var swapped = SwapNibbles(toSwap, registers);
        _mmuService.WriteByte(registers.HL, swapped);
        _clockService.Clock();
    }

    public void SwapA(ICpuRegistersService registers)
    {
        registers.A = SwapNibbles(registers.A, registers);
    }

    private byte SwapNibbles(byte toSwap, ICpuRegistersService registers)
    {
        var lowerShifted = (byte)(toSwap << 4);
        var upperShifted = (byte)(toSwap >> 4);
        var result = (byte)(lowerShifted + upperShifted);
        registers.F = new FlagsRegister()
        {
            Zero = result == 0,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        registers.ProgramCounter += 2;
        _clockService.Clock(2);
        return result;
    }
}