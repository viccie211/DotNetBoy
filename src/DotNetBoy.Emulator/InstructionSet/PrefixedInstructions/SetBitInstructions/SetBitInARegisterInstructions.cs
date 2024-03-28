using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInARegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInARegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC7, SetBit0InARegister },
            { 0xCF, SetBit1InARegister },
            { 0xD7, SetBit2InARegister },
            { 0xDF, SetBit3InARegister },
            { 0xE7, SetBit4InARegister },
            { 0xEF, SetBit5InARegister },
            { 0xF7, SetBit6InARegister },
            { 0xFF, SetBit7InARegister },
        };
    }

    public void SetBit0InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(0, registers.A, registers);
    }

    public void SetBit1InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(1, registers.A, registers);
    }

    public void SetBit2InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(2, registers.A, registers);
    }

    public void SetBit3InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(3, registers.A, registers);
    }

    public void SetBit4InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(4, registers.A, registers);
    }

    public void SetBit5InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(5, registers.A, registers);
    }

    public void SetBit6InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(6, registers.A, registers);
    }

    public void SetBit7InARegister(ICpuRegistersService registers)
    {
        registers.A = SetBit(7, registers.A, registers);
    }
}