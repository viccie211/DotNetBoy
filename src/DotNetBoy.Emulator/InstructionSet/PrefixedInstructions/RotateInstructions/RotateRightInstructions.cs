using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateRightInstructions(IClockService clockService, IMmuService mmuService) : RotateInstructionsBase(clockService), IInstructionSet
{
    public void RotateB(ICpuRegistersService registers)
    {
        registers.B = RotateRight(registers.B, registers);
    }

    public void RotateC(ICpuRegistersService registers)
    {
        registers.C = RotateRight(registers.C, registers);
    }

    public void RotateD(ICpuRegistersService registers)
    {
        registers.D = RotateRight(registers.D, registers);
    }

    public void RotateE(ICpuRegistersService registers)
    {
        registers.E = RotateRight(registers.E, registers);
    }

    public void RotateH(ICpuRegistersService registers)
    {
        registers.H = RotateRight(registers.H, registers);
    }

    public void RotateL(ICpuRegistersService registers)
    {
        registers.L = RotateRight(registers.L, registers);
    }

    public void RotateAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateRight(toRotate, registers);
        mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateA(ICpuRegistersService registers)
    {
        registers.A = RotateRight(registers.A, registers);
    }

    private byte RotateRight(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteRight(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x08:
                RotateB(registers);
                break;
            case 0x09:
                RotateC(registers);
                break;
            case 0x0A:
                RotateD(registers);
                break;
            case 0x0B:
                RotateE(registers);
                break;
            case 0x0C:
                RotateH(registers);
                break;
            case 0x0D:
                RotateL(registers);
                break;
            case 0x0E:
                RotateAtAddresssHL(registers);
                break;
            case 0x0F:
                RotateA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in RotateRightInstructions.");
        }
    }

}