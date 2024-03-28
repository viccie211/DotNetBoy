using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInBRegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInBRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC0, SetBit0InBRegister },
            { 0xC8, SetBit1InBRegister },
            { 0xD0, SetBit2InBRegister },
            { 0xD8, SetBit3InBRegister },
            { 0xE0, SetBit4InBRegister },
            { 0xE8, SetBit5InBRegister },
            { 0xF0, SetBit6InBRegister },
            { 0xF8, SetBit7InBRegister },
        };
    }

    public void SetBit0InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(0, registers.B, registers);
    }

    public void SetBit1InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(1, registers.B, registers);
    }

    public void SetBit2InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(2, registers.B, registers);
    }

    public void SetBit3InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(3, registers.B, registers);
    }

    public void SetBit4InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(4, registers.B, registers);
    }

    public void SetBit5InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(5, registers.B, registers);
    }

    public void SetBit6InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(6, registers.B, registers);
    }

    public void SetBit7InBRegister(ICpuRegistersService registers)
    {
        registers.B = SetBit(7, registers.B, registers);
    }
}