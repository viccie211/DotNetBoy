using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInLRegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInLRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x45, Bit0InLRegister },
            { 0x4D, Bit1InLRegister },
            { 0x55, Bit2InLRegister },
            { 0x5D, Bit3InLRegister },
            { 0x65, Bit4InLRegister },
            { 0x6D, Bit5InLRegister },
            { 0x75, Bit6InLRegister },
            { 0x7D, Bit7InLRegister },
        };
    }

    public void Bit0InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.L, registers);
    }

    public void Bit1InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.L, registers);
    }

    public void Bit2InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.L, registers);
    }

    public void Bit3InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.L, registers);
    }

    public void Bit4InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.L, registers);
    }

    public void Bit5InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.L, registers);
    }

    public void Bit6InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.L, registers);
    }

    public void Bit7InLRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.L, registers);
    }
}