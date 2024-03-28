using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;

public class SetBitInERegisterInstructions : SetBitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public SetBitInERegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0xC3, SetBit0InERegister },
            { 0xCB, SetBit1InERegister },
            { 0xD3, SetBit2InERegister },
            { 0xDB, SetBit3InERegister },
            { 0xE3, SetBit4InERegister },
            { 0xEB, SetBit5InERegister },
            { 0xF3, SetBit6InERegister },
            { 0xFB, SetBit7InERegister },
        };
    }

    public void SetBit0InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(0, registers.E, registers);
    }

    public void SetBit1InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(1, registers.E, registers);
    }

    public void SetBit2InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(2, registers.E, registers);
    }

    public void SetBit3InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(3, registers.E, registers);
    }

    public void SetBit4InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(4, registers.E, registers);
    }

    public void SetBit5InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(5, registers.E, registers);
    }

    public void SetBit6InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(6, registers.E, registers);
    }

    public void SetBit7InERegister(ICpuRegistersService registers)
    {
        registers.E = SetBit(7, registers.E, registers);
    }
}