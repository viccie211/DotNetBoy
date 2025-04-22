using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateLeftThroughCarryInstructions(IClockService clockService, IMmuService mmuService) : RotateInstructionsBase(clockService), IInstructionSet
{
    public void RotateThroughCarryB(ICpuRegistersService registers)
    {
        registers.B = RotateLeftThroughCarry(registers.B, registers);
    }

    public void RotateThroughCarryC(ICpuRegistersService registers)
    {
        registers.C = RotateLeftThroughCarry(registers.C, registers);
    }

    public void RotateThroughCarryD(ICpuRegistersService registers)
    {
        registers.D = RotateLeftThroughCarry(registers.D, registers);
    }

    public void RotateThroughCarryE(ICpuRegistersService registers)
    {
        registers.E = RotateLeftThroughCarry(registers.E, registers);
    }

    public void RotateThroughCarryH(ICpuRegistersService registers)
    {
        registers.H = RotateLeftThroughCarry(registers.H, registers);
    }

    public void RotateThroughCarryL(ICpuRegistersService registers)
    {
        registers.L = RotateLeftThroughCarry(registers.L, registers);
    }

    public void RotateThroughCarryAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateLeftThroughCarry(toRotate, registers);
        mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateThroughCarryA(ICpuRegistersService registers)
    {
        registers.A = RotateLeftThroughCarry(registers.A, registers);
    }

    private byte RotateLeftThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteLeftThroughCarry(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x10:
                RotateThroughCarryB(registers);
                break;
            case 0x11:
                RotateThroughCarryC(registers);
                break;
            case 0x12:
                RotateThroughCarryD(registers);
                break;
            case 0x13:
                RotateThroughCarryE(registers);
                break;
            case 0x14:
                RotateThroughCarryH(registers);
                break;
            case 0x15:
                RotateThroughCarryL(registers);
                break;
            case 0x16:
                RotateThroughCarryAtAddresssHL(registers);
                break;
            case 0x17:
                RotateThroughCarryA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in RotateLeftThroughCarryInstructions.");
        }
    }

}