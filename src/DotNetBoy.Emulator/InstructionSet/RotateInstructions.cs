using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class RotateInstructions(IClockService clockService) : RotateInstructionsBase(clockService)
{


    public void RotateALeft(ICpuRegistersService registers)
    {
        registers.A = RotateByteLeft(registers.A, registers, false);
    }

    public void RotateALeftThroughCarry(ICpuRegistersService registers)
    {
        registers.A = RotateByteLeftThroughCarry(registers.A, registers, false);
    }


    public void RotateARight(ICpuRegistersService registers)
    {
        registers.A = RotateByteRight(registers.A, registers, false);
    }

    public void RotateARightThroughCarry(ICpuRegistersService registers)
    {
        registers.A = RotateByteRightThroughCarry(registers.A, registers, false);
    }

    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x07:
                RotateALeft(registers);
                break;
            case 0x0F:
                RotateARight(registers);
                break;
            case 0x17:
                RotateALeftThroughCarry(registers);
                break;
            case 0x1F:
                RotateARightThroughCarry(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented.");
        }
    }
}