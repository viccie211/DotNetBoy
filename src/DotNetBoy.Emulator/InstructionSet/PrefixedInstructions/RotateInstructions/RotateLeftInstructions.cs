using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateLeftInstructions(IClockService clockService, IMmuService mmuService) : RotateInstructionsBase(clockService), IInstructionSet
{
    public void RotateB(ICpuRegistersService registers)
    {
        registers.B = RotateLeft(registers.B, registers);
    }

    public void RotateC(ICpuRegistersService registers)
    {
        registers.C = RotateLeft(registers.C, registers);
    }

    public void RotateD(ICpuRegistersService registers)
    {
        registers.D = RotateLeft(registers.D, registers);
    }

    public void RotateE(ICpuRegistersService registers)
    {
        registers.E = RotateLeft(registers.E, registers);
    }

    public void RotateH(ICpuRegistersService registers)
    {
        registers.H = RotateLeft(registers.H, registers);
    }

    public void RotateL(ICpuRegistersService registers)
    {
        registers.L = RotateLeft(registers.L, registers);
    }

    public void RotateAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateLeft(toRotate, registers);
        mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateA(ICpuRegistersService registers)
    {
        registers.A = RotateLeft(registers.A, registers);
    }

    private byte RotateLeft(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteLeft(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x00:
                RotateB(registers);
                break;
            case 0x01:
                RotateC(registers);
                break;
            case 0x02:
                RotateD(registers);
                break;
            case 0x03:
                RotateE(registers);
                break;
            case 0x04:
                RotateH(registers);
                break;
            case 0x05:
                RotateL(registers);
                break;
            case 0x06:
                RotateAtAddresssHL(registers);
                break;
            case 0x07:
                RotateA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in RotateLeftInstructions.");
        }
    }

}