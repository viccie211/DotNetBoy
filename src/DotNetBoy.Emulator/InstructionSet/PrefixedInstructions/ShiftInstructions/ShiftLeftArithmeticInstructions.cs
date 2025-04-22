using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftLeftArithmeticInstructions(IClockService clockService, IMmuService mmuService) : ShiftInstructionsBase(clockService), IInstructionSet
{
    public void ShiftLeftArithmeticB(ICpuRegistersService registers)
    {
        registers.B = ShiftLeftArithmeticByte(registers.B, registers);
    }

    public void ShiftLeftArithmeticC(ICpuRegistersService registers)
    {
        registers.C = ShiftLeftArithmeticByte(registers.C, registers);
    }

    public void ShiftLeftArithmeticD(ICpuRegistersService registers)
    {
        registers.D = ShiftLeftArithmeticByte(registers.D, registers);
    }

    public void ShiftLeftArithmeticE(ICpuRegistersService registers)
    {
        registers.E = ShiftLeftArithmeticByte(registers.E, registers);
    }

    public void ShiftLeftArithmeticH(ICpuRegistersService registers)
    {
        registers.H = ShiftLeftArithmeticByte(registers.H, registers);
    }

    public void ShiftLeftArithmeticL(ICpuRegistersService registers)
    {
        registers.L = ShiftLeftArithmeticByte(registers.L, registers);
    }

    public void ShiftLeftArithmeticAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftLeftArithmeticByte(toSRL, registers);
        mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }

    public void ShiftLeftArithmeticA(ICpuRegistersService registers)
    {
        registers.A = ShiftLeftArithmeticByte(registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x20:
                ShiftLeftArithmeticB(registers);
                break;
            case 0x21:
                ShiftLeftArithmeticC(registers);
                break;
            case 0x22:
                ShiftLeftArithmeticD(registers);
                break;
            case 0x23:
                ShiftLeftArithmeticE(registers);
                break;
            case 0x24:
                ShiftLeftArithmeticH(registers);
                break;
            case 0x25:
                ShiftLeftArithmeticL(registers);
                break;
            case 0x26:
                ShiftLeftArithmeticAtAddressHL(registers);
                break;
            case 0x27:
                ShiftLeftArithmeticA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ShiftLeftArithmeticInstructions.");
        }
    }
}