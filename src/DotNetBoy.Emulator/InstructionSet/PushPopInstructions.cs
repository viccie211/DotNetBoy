using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class PushPopInstructions(IClockService clockService, IMmuService mmuService, IByteUshortService byteUshortService) : IInstructionSet
{
    /// <summary>
    /// Pop the first word of the stack and store it in the BC register
    /// </summary>
    /// Verified against BGB
    public void PopBC(ICpuRegistersService registers)
    {
        registers.BC = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the BC register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushBC(ICpuRegistersService registers)
    {
        PushWord(registers.BC, registers);
    }
    
    /// <summary>
    /// Pop the first word of the stack and store it in the DE register
    /// </summary>
    /// 
    public void PopDE(ICpuRegistersService registers)
    {
        registers.DE = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the DE register to the stack
    /// </summary>
    /// 
    public void PushDE(ICpuRegistersService registers)
    {
        PushWord(registers.DE, registers);
    }

    /// <summary>
    /// Pop the first word of the stack and store it in the HL register
    /// </summary>
    /// Verified against BGB
    public void PopHL(ICpuRegistersService registers)
    {
        registers.HL = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the HL register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushHL(ICpuRegistersService registers)
    {
        PushWord(registers.HL, registers);
    }

    /// <summary>
    /// Pop the first word of the stack and store it in the AF register
    /// </summary>
    /// Verified against BGB
    public void PopAF(ICpuRegistersService registers)
    {
        registers.AF = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the AF register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushAF(ICpuRegistersService registers)
    {
        PushWord(registers.AF, registers);
    }

    private void PushWord(ushort wordToPush, ICpuRegistersService registers)
    {
        var upper = byteUshortService.UpperByteOfSixteenBits(wordToPush);
        var lower = byteUshortService.LowerByteOfSixteenBits(wordToPush);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter += 1;
        clockService.Clock(3);
    }

    private ushort PopWord(ICpuRegistersService registers)
    {
        var lower = mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        var upper = mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        registers.ProgramCounter += 1;
        clockService.Clock(2);
        return byteUshortService.CombineBytes(upper, lower);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0xC1:
                PopBC(registers);
                break;
            case 0xC5:
                PushBC(registers);
                break;
            case 0xD1:
                PopDE(registers);
                break;
            case 0xD5:
                PushDE(registers);
                break;
            case 0xE1:
                PopHL(registers);
                break;
            case 0xE5:
                PushHL(registers);
                break;
            case 0xF1:
                PopAF(registers);
                break;
            case 0xF5:
                PushAF(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in PushPopInstructions.");
        }
    }

}