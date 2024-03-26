using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInARegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    private readonly IClockService _clockService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInARegisterInstructions(IClockService clockService)
    {
        _clockService = clockService;

        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x87, ResetBit0InARegister },
            { 0x8F, ResetBit1InARegister },
            { 0x97, ResetBit2InARegister },
            { 0x9F, ResetBit3InARegister },
            { 0xA7, ResetBit4InARegister },
            { 0xAF, ResetBit5InARegister },
            { 0xB7, ResetBit6InARegister },
            { 0xBF, ResetBit7InARegister },
        };
    }

    public void ResetBit0InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(0);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit1InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(1);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit2InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(2);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit3InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(3);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit4InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(4);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit5InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(5);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit6InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(6);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }

    public void ResetBit7InARegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(7);
        registers.A = (byte)(registers.A & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }
}