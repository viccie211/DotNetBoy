using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInDRegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInDRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x82, ResetBit0InDRegister },
            { 0x8A, ResetBit1InDRegister },
            { 0x92, ResetBit2InDRegister },
            { 0x9A, ResetBit3InDRegister },
            { 0xA2, ResetBit4InDRegister },
            { 0xAA, ResetBit5InDRegister },
            { 0xB2, ResetBit6InDRegister },
            { 0xBA, ResetBit7InDRegister },
        };
    }

    public void ResetBit0InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(0, registers.D, registers);
    }

    public void ResetBit1InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(1, registers.D, registers);
    }

    public void ResetBit2InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(2, registers.D, registers);
    }

    public void ResetBit3InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(3, registers.D, registers);
    }

    public void ResetBit4InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(4, registers.D, registers);
    }

    public void ResetBit5InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(5, registers.D, registers);
    }

    public void ResetBit6InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(6, registers.D, registers);
    }

    public void ResetBit7InDRegister(ICpuRegistersService registers)
    {
        registers.D = ResetBit(7, registers.D, registers);
    }
}