using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInCRegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInCRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x81, ResetBit0InCRegister },
            { 0x89, ResetBit1InCRegister },
            { 0x91, ResetBit2InCRegister },
            { 0x99, ResetBit3InCRegister },
            { 0xA1, ResetBit4InCRegister },
            { 0xA9, ResetBit5InCRegister },
            { 0xB1, ResetBit6InCRegister },
            { 0xB9, ResetBit7InCRegister },
        };
    }

    public void ResetBit0InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(0, registers.C, registers);
    }

    public void ResetBit1InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(1, registers.C, registers);
    }

    public void ResetBit2InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(2, registers.C, registers);
    }

    public void ResetBit3InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(3, registers.C, registers);
    }

    public void ResetBit4InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(4, registers.C, registers);
    }

    public void ResetBit5InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(5, registers.C, registers);
    }

    public void ResetBit6InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(6, registers.C, registers);
    }

    public void ResetBit7InCRegister(ICpuRegistersService registers)
    {
        registers.C = ResetBit(7, registers.C, registers);
    }
}