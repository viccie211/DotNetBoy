using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInLRegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInLRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x85, ResetBit0InLRegister },
            { 0x8D, ResetBit1InLRegister },
            { 0x95, ResetBit2InLRegister },
            { 0x9D, ResetBit3InLRegister },
            { 0xA5, ResetBit4InLRegister },
            { 0xAD, ResetBit5InLRegister },
            { 0xB5, ResetBit6InLRegister },
            { 0xBD, ResetBit7InLRegister },
        };
    }

    public void ResetBit0InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(0, registers.L, registers);
    }

    public void ResetBit1InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(1, registers.L, registers);
    }

    public void ResetBit2InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(2, registers.L, registers);
    }

    public void ResetBit3InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(3, registers.L, registers);
    }

    public void ResetBit4InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(4, registers.L, registers);
    }

    public void ResetBit5InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(5, registers.L, registers);
    }

    public void ResetBit6InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(6, registers.L, registers);
    }

    public void ResetBit7InLRegister(ICpuRegistersService registers)
    {
        registers.L = ResetBit(7, registers.L, registers);
    }
}