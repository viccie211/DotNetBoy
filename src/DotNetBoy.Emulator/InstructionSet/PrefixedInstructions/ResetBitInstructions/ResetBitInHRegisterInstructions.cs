using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInHRegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInHRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x84, ResetBit0InHRegister },
            { 0x8C, ResetBit1InHRegister },
            { 0x94, ResetBit2InHRegister },
            { 0x9C, ResetBit3InHRegister },
            { 0xA4, ResetBit4InHRegister },
            { 0xAC, ResetBit5InHRegister },
            { 0xB4, ResetBit6InHRegister },
            { 0xBC, ResetBit7InHRegister },
        };
    }

    public void ResetBit0InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(0, registers.H, registers);
    }

    public void ResetBit1InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(1, registers.H, registers);
    }

    public void ResetBit2InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(2, registers.H, registers);
    }

    public void ResetBit3InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(3, registers.H, registers);
    }

    public void ResetBit4InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(4, registers.H, registers);
    }

    public void ResetBit5InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(5, registers.H, registers);
    }

    public void ResetBit6InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(6, registers.H, registers);
    }

    public void ResetBit7InHRegister(ICpuRegistersService registers)
    {
        registers.H = ResetBit(7, registers.H, registers);
    }
}