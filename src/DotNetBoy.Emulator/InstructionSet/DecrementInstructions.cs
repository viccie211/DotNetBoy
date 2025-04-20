using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class DecrementInstructions(IClockService clockService, IMmuService mmuService) : IInstructionSet
{
    /// <summary>
    /// Decrement the contents of the B register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementB(ICpuRegistersService registers)
    {
        registers.B = Decrement8Bits(registers.B, registers);
    }

    /// <summary>
    /// Decrement the contents of the C register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementC(ICpuRegistersService registers)
    {
        registers.C = Decrement8Bits(registers.C, registers);
    }

    /// <summary>
    /// Decrement the contents of the D register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementD(ICpuRegistersService registers)
    {
        registers.D = Decrement8Bits(registers.D, registers);
    }

    /// <summary>
    /// Decrement the contents of the E register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementE(ICpuRegistersService registers)
    {
        registers.E = Decrement8Bits(registers.E, registers);
    }

    /// <summary>
    /// Decrement the contents of the H register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementH(ICpuRegistersService registers)
    {
        registers.H = Decrement8Bits(registers.H, registers);
    }

    /// <summary>
    /// Decrement the contents of the L register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementL(ICpuRegistersService registers)
    {
        registers.L = Decrement8Bits(registers.L, registers);
    }

    /// <summary>
    /// Decrement the contents memory specified by the HL register register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementAtAddressHL(ICpuRegistersService registers)
    {
        var toDecrement = mmuService.ReadByte(registers.HL);
        mmuService.WriteByte(registers.HL, Decrement8Bits(toDecrement, registers));
        clockService.Clock(2);
    }

    /// <summary>
    /// Decrement the contents of the A register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementA(ICpuRegistersService registers)
    {
        registers.A = Decrement8Bits(registers.A, registers);
    }

    public void DecrementBC(ICpuRegistersService registers)
    {
        registers.BC = Decrement16Bits(registers.BC, registers);
    }

    public void DecrementDE(ICpuRegistersService registers)
    {
        registers.DE = Decrement16Bits(registers.DE, registers);
    }

    public void DecrementHL(ICpuRegistersService registers)
    {
        registers.HL = Decrement16Bits(registers.HL, registers);
    }

    public void DecrementStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = Decrement16Bits(registers.StackPointer, registers);
    }

    private byte Decrement8Bits(byte initial, ICpuRegistersService registers)
    {
        registers.F.Subtract = true;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(initial, 0x01);
        var result = (byte)(initial - 1);
        registers.F.Zero = result == 0;
        registers.ProgramCounter += 1;
        return result;
    }

    private ushort Decrement16Bits(ushort initial, ICpuRegistersService registers)
    {
        var result = (ushort)(initial - 1);
        clockService.Clock();
        registers.ProgramCounter += 1;
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x05:
                DecrementB(registers);
                break;
            case 0x0B:
                DecrementBC(registers);
                break;
            case 0x0D:
                DecrementC(registers);
                break;
            case 0x15:
                DecrementD(registers);
                break;
            case 0x1B:
                DecrementDE(registers);
                break;
            case 0x1D:
                DecrementE(registers);
                break;
            case 0x25:
                DecrementH(registers);
                break;
            case 0x2B:
                DecrementHL(registers);
                break;
            case 0x2D:
                DecrementL(registers);
                break;
            case 0x35:
                DecrementAtAddressHL(registers);
                break;
            case 0x3B:
                DecrementStackPointer(registers);
                break;
            case 0x3D:
                DecrementA(registers);
                break;

            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in DecrementInstructions.");
        }
    }

}