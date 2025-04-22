using DotNetBoy.Emulator.InstructionSet.Abstracts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;

public class RotateRightThroughCarryInstructions(IClockService clockService, IMmuService mmuService) : RotateInstructionsBase(clockService), IInstructionSet
{
    public void RotateThroughCarryB(ICpuRegistersService registers)
    {
        registers.B = RotateRightThroughCarry(registers.B, registers);
    }

    public void RotateThroughCarryC(ICpuRegistersService registers)
    {
        registers.C = RotateRightThroughCarry(registers.C, registers);
    }

    public void RotateThroughCarryD(ICpuRegistersService registers)
    {
        registers.D = RotateRightThroughCarry(registers.D, registers);
    }

    public void RotateThroughCarryE(ICpuRegistersService registers)
    {
        registers.E = RotateRightThroughCarry(registers.E, registers);
    }

    public void RotateThroughCarryH(ICpuRegistersService registers)
    {
        registers.H = RotateRightThroughCarry(registers.H, registers);
    }

    public void RotateThroughCarryL(ICpuRegistersService registers)
    {
        registers.L = RotateRightThroughCarry(registers.L, registers);
    }

    public void RotateThroughCarryAtAddresssHL(ICpuRegistersService registers)
    {
        var toRotate = mmuService.ReadByte(registers.HL);
        ClockService.Clock();
        var rotated = RotateRightThroughCarry(toRotate, registers);
        mmuService.WriteByte(registers.HL, rotated);
        ClockService.Clock();
    }

    public void RotateThroughCarryA(ICpuRegistersService registers)
    {
        registers.A = RotateRightThroughCarry(registers.A, registers);
    }

    private byte RotateRightThroughCarry(byte toRotate, ICpuRegistersService registers)
    {
        var result = RotateByteRightThroughCarry(toRotate, registers);
        registers.ProgramCounter += 1;
        ClockService.Clock();
        return result;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x18:
                RotateThroughCarryB(registers);
                break;
            case 0x19:
                RotateThroughCarryC(registers);
                break;
            case 0x1A:
                RotateThroughCarryD(registers);
                break;
            case 0x1B:
                RotateThroughCarryE(registers);
                break;
            case 0x1C:
                RotateThroughCarryH(registers);
                break;
            case 0x1D:
                RotateThroughCarryL(registers);
                break;
            case 0x1E:
                RotateThroughCarryAtAddresssHL(registers);
                break;
            case 0x1F:
                RotateThroughCarryA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in RotateRightThroughCarryInstructions.");
        }
    }

}