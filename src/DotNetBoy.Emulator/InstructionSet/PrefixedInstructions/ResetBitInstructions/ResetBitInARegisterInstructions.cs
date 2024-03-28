using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInARegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInARegisterInstructions(IClockService clockService) : base(clockService)
    {
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
        registers.A = ResetBit(0, registers.A, registers);
    }

    public void ResetBit1InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(1, registers.A, registers);
    }

    public void ResetBit2InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(2, registers.A, registers);
    }

    public void ResetBit3InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(3, registers.A, registers);
    }

    public void ResetBit4InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(4, registers.A, registers);
    }

    public void ResetBit5InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(5, registers.A, registers);
    }

    public void ResetBit6InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(6, registers.A, registers);
    }

    public void ResetBit7InARegister(ICpuRegistersService registers)
    {
        registers.A = ResetBit(7, registers.A, registers);
    }
}