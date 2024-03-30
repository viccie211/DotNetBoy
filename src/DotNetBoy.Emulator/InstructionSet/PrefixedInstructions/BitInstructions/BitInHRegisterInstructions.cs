using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;

public class BitInHRegisterInstructions : BitInstructionsBase, IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public BitInHRegisterInstructions(IClockService clockService) : base(clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x44, Bit0InHRegister },
            { 0x4C, Bit1InHRegister },
            { 0x54, Bit2InHRegister },
            { 0x5C, Bit3InHRegister },
            { 0x64, Bit4InHRegister },
            { 0x6C, Bit5InHRegister },
            { 0x74, Bit6InHRegister },
            { 0x7C, Bit7InHRegister },
        };
    }

    public void Bit0InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(0, registers.H, registers);
    }

    public void Bit1InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(1, registers.H, registers);
    }

    public void Bit2InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(2, registers.H, registers);
    }

    public void Bit3InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(3, registers.H, registers);
    }

    public void Bit4InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(4, registers.H, registers);
    }

    public void Bit5InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(5, registers.H, registers);
    }

    public void Bit6InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(6, registers.H, registers);
    }

    public void Bit7InHRegister(ICpuRegistersService registers)
    {
        SetComplementOfBitToZeroFlag(7, registers.H, registers);
    }
}