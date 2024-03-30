using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInARegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInARegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x47, Bit0InARegister },
            { 0x4F, Bit1InARegister },
            { 0x57, Bit2InARegister },
            { 0x5F, Bit3InARegister },
            { 0x67, Bit4InARegister },
            { 0x6F, Bit5InARegister },
            { 0x77, Bit6InARegister },
            { 0x7F, Bit7InARegister },
        };
    }

    public void Bit0InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.A, registers);
    }

    public void Bit1InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.A, registers);
    }

    public void Bit2InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.A, registers);
    }

    public void Bit3InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.A, registers);
    }

    public void Bit4InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.A, registers);
    }

    public void Bit5InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.A, registers);
    }

    public void Bit6InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.A, registers);
    }

    public void Bit7InARegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.A, registers);
    }
}