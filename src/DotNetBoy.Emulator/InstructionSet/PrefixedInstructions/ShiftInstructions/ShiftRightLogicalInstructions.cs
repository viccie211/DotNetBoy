using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;

public class ShiftRightLogicalInstructions(IClockService clockService, IMmuService mmuService) : ShiftInstructionsBase(clockService), IInstructionSet
{
    public void ShiftRightLogicalB(ICpuRegistersService registers)
    {
        registers.B = ShiftRightLogicalByte(registers.B, registers);
    }

    public void ShiftRightLogicalC(ICpuRegistersService registers)
    {
        registers.C = ShiftRightLogicalByte(registers.C, registers);
    }

    public void ShiftRightLogicalD(ICpuRegistersService registers)
    {
        registers.D = ShiftRightLogicalByte(registers.D, registers);
    }

    public void ShiftRightLogicalE(ICpuRegistersService registers)
    {
        registers.E = ShiftRightLogicalByte(registers.E, registers);
    }

    public void ShiftRightLogicalH(ICpuRegistersService registers)
    {
        registers.H = ShiftRightLogicalByte(registers.H, registers);
    }

    public void ShiftRightLogicalL(ICpuRegistersService registers)
    {
        registers.L = ShiftRightLogicalByte(registers.L, registers);
    }

    public void ShiftRightLogicalAtAddressHL(ICpuRegistersService registers)
    {
        var toSRL = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var srled = ShiftRightLogicalByte(toSRL, registers);
        mmuService.WriteByte(registers.HL, srled);
        ClockService.Clock();
    }
    
    public void ShiftRightLogicalA(ICpuRegistersService registers)
    {
        registers.A = ShiftRightLogicalByte(registers.A, registers);
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x38:
                ShiftRightLogicalB(registers);
                break;
            case 0x39:
                ShiftRightLogicalC(registers);
                break;
            case 0x3A:
                ShiftRightLogicalD(registers);
                break;
            case 0x3B:
                ShiftRightLogicalE(registers);
                break;
            case 0x3C:
                ShiftRightLogicalH(registers);
                break;
            case 0x3D:
                ShiftRightLogicalL(registers);
                break;
            case 0x3E:
                ShiftRightLogicalAtAddressHL(registers);
                break;
            case 0x3F:
                ShiftRightLogicalA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in ShiftRightLogicalInstructions.");
        }
    }

}