using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;

public class SwapInstructions(IClockService clockService, IMmuService mmuService) : IInstructionSet
{
    public void SwapB(ICpuRegistersService registers)
    {
        registers.B = SwapNibbles(registers.B, registers);
    }

    public void SwapC(ICpuRegistersService registers)
    {
        registers.C = SwapNibbles(registers.C, registers);
    }

    public void SwapD(ICpuRegistersService registers)
    {
        registers.D = SwapNibbles(registers.D, registers);
    }

    public void SwapE(ICpuRegistersService registers)
    {
        registers.E = SwapNibbles(registers.E, registers);
    }

    public void SwapH(ICpuRegistersService registers)
    {
        registers.H = SwapNibbles(registers.H, registers);
    }

    public void SwapL(ICpuRegistersService registers)
    {
        registers.L = SwapNibbles(registers.L, registers);
    }

    public void SwapAtAddressHL(ICpuRegistersService registers)
    {
        var toSwap = mmuService.ReadByte(registers.HL);
        clockService.Clock();
        var swapped = SwapNibbles(toSwap, registers);
        mmuService.WriteByte(registers.HL, swapped);
        clockService.Clock();
    }

    public void SwapA(ICpuRegistersService registers)
    {
        registers.A = SwapNibbles(registers.A, registers);
    }

    private byte SwapNibbles(byte toSwap, ICpuRegistersService registers)
    {
        var lowerShifted = (byte)(toSwap << 4);
        var upperShifted = (byte)(toSwap >> 4);
        var result = (byte)(lowerShifted + upperShifted);
        registers.F = new FlagsRegister()
        {
            Zero = result == 0,
            Subtract = false,
            HalfCarry = false,
            Carry = false,
        };
        registers.ProgramCounter += 2;
        clockService.Clock(1);
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x30:
                SwapB(registers);
                break;
            case 0x31:
                SwapC(registers);
                break;
            case 0x32:
                SwapD(registers);
                break;
            case 0x33:
                SwapE(registers);
                break;
            case 0x34:
                SwapH(registers);
                break;
            case 0x35:
                SwapL(registers);
                break;
            case 0x36:
                SwapAtAddressHL(registers);
                break;
            case 0x37:
                SwapA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in SwapInstructions.");
        }
    }

}