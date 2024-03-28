using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInERegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInERegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x83, ResetBit0InERegister },
            { 0x8B, ResetBit1InERegister },
            { 0x93, ResetBit2InERegister },
            { 0x9B, ResetBit3InERegister },
            { 0xE3, ResetBit4InERegister },
            { 0xEB, ResetBit5InERegister },
            { 0xB3, ResetBit6InERegister },
            { 0xBB, ResetBit7InERegister },
        };
    }

    public void ResetBit0InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(0, registers.E, registers);
    }

    public void ResetBit1InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(1, registers.E, registers);
    }

    public void ResetBit2InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(2, registers.E, registers);
    }

    public void ResetBit3InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(3, registers.E, registers);
    }

    public void ResetBit4InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(4, registers.E, registers);
    }

    public void ResetBit5InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(5, registers.E, registers);
    }

    public void ResetBit6InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(6, registers.E, registers);
    }

    public void ResetBit7InERegister(ICpuRegistersService registers)
    {
        registers.E = ResetBit(7, registers.E, registers);
    }
}