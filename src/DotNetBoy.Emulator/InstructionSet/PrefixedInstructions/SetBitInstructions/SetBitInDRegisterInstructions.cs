using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInDRegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInDRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC2, SetBit0InDRegister },
            { 0xCA, SetBit1InDRegister },
            { 0xD2, SetBit2InDRegister },
            { 0xDA, SetBit3InDRegister },
            { 0xE2, SetBit4InDRegister },
            { 0xEA, SetBit5InDRegister },
            { 0xF2, SetBit6InDRegister },
            { 0xFA, SetBit7InDRegister },
        };
    }

    public void SetBit0InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(0, registers.D, registers);
    }

    public void SetBit1InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(1, registers.D, registers);
    }

    public void SetBit2InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(2, registers.D, registers);
    }

    public void SetBit3InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(3, registers.D, registers);
    }

    public void SetBit4InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(4, registers.D, registers);
    }

    public void SetBit5InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(5, registers.D, registers);
    }

    public void SetBit6InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(6, registers.D, registers);
    }

    public void SetBit7InDRegister(ICpuRegistersService registers)
    {
        registers.D = SetBit(7, registers.D, registers);
    }
}