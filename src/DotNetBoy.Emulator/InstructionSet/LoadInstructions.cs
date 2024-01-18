namespace DotNetBoy.Emulator.InstructionSet;

public class LoadInstructions
{
    // /// <summary>
    // /// Load a word from memory into the BC register
    // /// </summary>
    // public static void LoadD16IntoBC(Cpu cpu)
    // {
    //     cpu.regBC = cpu.MmuService.ReadWordLittleEndian((ushort)(cpu.regPC + 1));
    //     cpu.Clock(3);
    //     cpu.regPC += 3;
    // }
    //
    // public static void LoadD16IntoSP(Cpu cpu)
    // {
    //     cpu.regSP = cpu.MmuService.ReadWordLittleEndian((ushort)(cpu.regPC + 1));
    //     cpu.Clock(3);
    //     cpu.regPC += 3;
    // }
    //
    // /// <summary>
    // /// Load a byte at the address in the BC register into the A register
    // /// </summary>
    // public static void LoadAtAddressBCIntoA(Cpu cpu)
    // {
    //     cpu.regA = cpu.MmuService.ReadByte(cpu.regBC);
    //     cpu.Clock(2);
    //     cpu.regPC += 1;
    // }
    //
    // public static void LoadD8IntoB(Cpu cpu)
    // {
    //     cpu.regB = cpu.MmuService.ReadByte((ushort)(cpu.regPC + 1));
    //     cpu.Clock(2);
    //     cpu.regPC += 2;
    // }
    //
    // /// <summary>
    // /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    // /// </summary>
    // public static void LoadSomeValueIntoA(Cpu cpu)
    // {
    //     var address = (ushort)(0xFF00 + cpu.MmuService.ReadByte((ushort)(cpu.regPC + 1)));
    //     cpu.regA = cpu.MmuService.ReadByte(address);
    //     cpu.Clock(3);
    //     cpu.regPC += 2;
    // }
}