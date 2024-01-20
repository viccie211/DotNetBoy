using DotNetBoy.Emulator.InstructionSet.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateAndShiftInstructions :IInstructionSet
{
    public RotateAndShiftInstructions()
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x07, RotateLeftWithCarryA }
        };
    }
    private void RotateLeftWithCarryA(CpuRegisters cpu)
    {
        cpu.F.Carry = (cpu.A & 0x80) == 0x80;
        var tempByte = (byte)(cpu.A << 1);
        cpu.A = cpu.F.Carry ? (byte)(tempByte | 0x01) : (byte)(tempByte ^ 0x01);
        cpu.F.Zero = false;
        cpu.F.HalfCarry = false;
        cpu.F.Subtract = false;
        cpu.Clock();
        cpu.ProgramCounter += 1;
    }
    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}