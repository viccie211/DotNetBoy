namespace DotNetBoy.Emulator.InstructionSet;

public class StoreInstructions
{
    // public static void StoreStackPointerAtAddress(Cpu cpu)
    // {
    //     var lower = cpu.ByteUshortService.LowerByteOfSixteenBits(cpu.regSP);
    //     var upper = cpu.ByteUshortService.UpperByteOfSixteenBits(cpu.regSP);
    //     var targetAddress = cpu.MmuService.ReadWordLittleEndian((ushort)(cpu.regPC + 1));
    //     cpu.MmuService.WriteByte(targetAddress,lower);
    //     cpu.MmuService.WriteByte((ushort)(targetAddress+1),upper);
    //     cpu.Clock(5);
    //     cpu.regPC += 3;
    // }
}