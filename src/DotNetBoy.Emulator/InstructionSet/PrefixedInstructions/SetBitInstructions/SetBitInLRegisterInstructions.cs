using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInLRegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInLRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC5, SetBit0InLRegister },
            { 0xCD, SetBit1InLRegister },
            { 0xD5, SetBit2InLRegister },
            { 0xDD, SetBit3InLRegister },
            { 0xE5, SetBit4InLRegister },
            { 0xED, SetBit5InLRegister },
            { 0xF5, SetBit6InLRegister },
            { 0xFD, SetBit7InLRegister },
        };
    }

    public void SetBit0InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(0, registers.L, registers);
    }

    public void SetBit1InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(1, registers.L, registers);
    }

    public void SetBit2InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(2, registers.L, registers);
    }

    public void SetBit3InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(3, registers.L, registers);
    }

    public void SetBit4InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(4, registers.L, registers);
    }

    public void SetBit5InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(5, registers.L, registers);
    }

    public void SetBit6InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(6, registers.L, registers);
    }

    public void SetBit7InLRegister(ICpuRegistersService registers)
    {
        registers.L = SetBit(7, registers.L, registers);
    }
}