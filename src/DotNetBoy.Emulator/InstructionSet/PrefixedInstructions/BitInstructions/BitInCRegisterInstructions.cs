using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInCRegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInCRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x41, Bit0InCRegister },
            { 0x49, Bit1InCRegister },
            { 0x51, Bit2InCRegister },
            { 0x59, Bit3InCRegister },
            { 0x61, Bit4InCRegister },
            { 0x69, Bit5InCRegister },
            { 0x71, Bit6InCRegister },
            { 0x79, Bit7InCRegister },
        };
    }

    public void Bit0InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.C, registers);
    }

    public void Bit1InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.C, registers);
    }

    public void Bit2InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.C, registers);
    }

    public void Bit3InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.C, registers);
    }

    public void Bit4InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.C, registers);
    }

    public void Bit5InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.C, registers);
    }

    public void Bit6InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.C, registers);
    }

    public void Bit7InCRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.C, registers);
    }
}