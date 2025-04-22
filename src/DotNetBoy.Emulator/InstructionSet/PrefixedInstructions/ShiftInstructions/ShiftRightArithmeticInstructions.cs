using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftRightArithmeticInstructions(IClockService clockService, IMmuService mmuService) : ShiftInstructionsBase(clockService), IInstructionSet
{
    public void ShiftRightArithmeticB(ICpuRegistersService registers)
    {
        registers.B = ShiftRightArithmeticByte(registers.B, registers);
    }

    public void ShiftRightArithmeticC(ICpuRegistersService registers)
    {
        registers.C = ShiftRightArithmeticByte(registers.C, registers);
    }

    public void ShiftRightArithmeticD(ICpuRegistersService registers)
    {
        registers.D = ShiftRightArithmeticByte(registers.D, registers);
    }

    public void ShiftRightArithmeticE(ICpuRegistersService registers)
    {
        registers.E = ShiftRightArithmeticByte(registers.E, registers);
    }

    public void ShiftRightArithmeticH(ICpuRegistersService registers)
    {
        registers.H = ShiftRightArithmeticByte(registers.H, registers);
    }

    public void ShiftRightArithmeticL(ICpuRegistersService registers)
    {
        registers.L = ShiftRightArithmeticByte(registers.L, registers);
    }

    public void ShiftRightArithmeticAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftRightArithmeticByte(toSRL, registers);
        mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }

    public void ShiftRightArithmeticA(ICpuRegistersService registers)
    {
        registers.A = ShiftRightArithmeticByte(registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x28:
                ShiftRightArithmeticB(registers);
                break;
            case 0x29:
                ShiftRightArithmeticC(registers);
                break;
            case 0x2A:
                ShiftRightArithmeticD(registers);
                break;
            case 0x2B:
                ShiftRightArithmeticE(registers);
                break;
            case 0x2C:
                ShiftRightArithmeticH(registers);
                break;
            case 0x2D:
                ShiftRightArithmeticL(registers);
                break;
            case 0x2E:
                ShiftRightArithmeticAtAddressHL(registers);
                break;
            case 0x2F:
                ShiftRightArithmeticA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ShiftRightArithmeticInstructions.");
        }
    }

}