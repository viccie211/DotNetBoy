using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInCRegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInCRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC1, SetBit0InCRegister },
            { 0xC9, SetBit1InCRegister },
            { 0xD1, SetBit2InCRegister },
            { 0xD9, SetBit3InCRegister },
            { 0xE1, SetBit4InCRegister },
            { 0xE9, SetBit5InCRegister },
            { 0xF1, SetBit6InCRegister },
            { 0xF9, SetBit7InCRegister },
        };
    }

    public void SetBit0InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(0, registers.C, registers);
    }

    public void SetBit1InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(1, registers.C, registers);
    }

    public void SetBit2InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(2, registers.C, registers);
    }

    public void SetBit3InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(3, registers.C, registers);
    }

    public void SetBit4InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(4, registers.C, registers);
    }

    public void SetBit5InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(5, registers.C, registers);
    }

    public void SetBit6InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(6, registers.C, registers);
    }

    public void SetBit7InCRegister(ICpuRegistersService registers)
    {
        registers.C = SetBit(7, registers.C, registers);
    }
}