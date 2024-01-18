namespace DotNetBoy.Emulator.InstructionSet;

public class RotateAndShiftInstructions
{
    // public static void RotateLeftWithCarryA(Cpu cpu)
    // {
    //     cpu.regF.Carry = (cpu.regA & 0x80) == 0x80;
    //     var tempByte = (byte)(cpu.regA << 1);
    //     cpu.regA = cpu.regF.Carry ? (byte)(tempByte | 0x01) : (byte)(tempByte ^ 0x01);
    //     cpu.regF.Zero = false;
    //     cpu.regF.HalfCarry = false;
    //     cpu.regF.Subtract = false;
    //     cpu.Clock();
    //     cpu.regPC += 1;
    // }
}