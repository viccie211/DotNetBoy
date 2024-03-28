using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInHRegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInHRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC4, SetBit0InHRegister },
            { 0xCC, SetBit1InHRegister },
            { 0xD4, SetBit2InHRegister },
            { 0xDC, SetBit3InHRegister },
            { 0xE4, SetBit4InHRegister },
            { 0xEC, SetBit5InHRegister },
            { 0xF4, SetBit6InHRegister },
            { 0xFC, SetBit7InHRegister },
        };
    }

    public void SetBit0InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(0, registers.H, registers);
    }

    public void SetBit1InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(1, registers.H, registers);
    }

    public void SetBit2InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(2, registers.H, registers);
    }

    public void SetBit3InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(3, registers.H, registers);
    }

    public void SetBit4InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(4, registers.H, registers);
    }

    public void SetBit5InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(5, registers.H, registers);
    }

    public void SetBit6InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(6, registers.H, registers);
    }

    public void SetBit7InHRegister(ICpuRegistersService registers)
    {
        registers.H = SetBit(7, registers.H, registers);
    }
}