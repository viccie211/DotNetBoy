using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInERegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInERegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x43, Bit0InERegister },
            { 0x4B, Bit1InERegister },
            { 0x53, Bit2InERegister },
            { 0x5B, Bit3InERegister },
            { 0x63, Bit4InERegister },
            { 0x6B, Bit5InERegister },
            { 0x73, Bit6InERegister },
            { 0x7B, Bit7InERegister },
        };
    }

    public void Bit0InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.E, registers);
    }

    public void Bit1InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.E, registers);
    }

    public void Bit2InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.E, registers);
    }

    public void Bit3InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.E, registers);
    }

    public void Bit4InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.E, registers);
    }

    public void Bit5InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.E, registers);
    }

    public void Bit6InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.E, registers);
    }

    public void Bit7InERegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.E, registers);
    }
}