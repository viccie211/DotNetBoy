using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class MiscellaneousInstructions(IClockService clockService, IMmuService mmuService) : IInstructionSet
{
    /// <summary>
    /// Does nothing but increment the Program Counter and pump the clock one cycle
    /// </summary>
    /// Verified against BGB
    public void NOP(ICpuRegistersService registers)
    {
        registers.ProgramCounter++;
    }

    public void DAA(ICpuRegistersService registers)
    {
        // note: assumes a is a uint8_t and wraps from 0xff to 0
        if (!registers.F.Subtract)
        {
            // after an addition, adjust if (half-)carry occurred or if result is out of bounds
            if (registers.F.Carry || registers.A > 0x99)
            {
                registers.A += 0x60;
                registers.F.Carry = true;
            }

            if (registers.F.HalfCarry || (registers.A & 0x0f) > 0x09)
            {
                registers.A += 0x6;
            }
        }
        else
        {
            // after a subtraction, only adjust if (half-)carry occurred
            if (registers.F.Carry)
            {
                registers.A -= 0x60;
            }

            if (registers.F.HalfCarry)
            {
                registers.A -= 0x6;
            }
        }

        // these flags are always updated
        registers.F.Zero = registers.A == 0; // the usual z flag
        registers.F.HalfCarry = false; // h flag is always cleared
        registers.ProgramCounter += 1;
    }

    public void ComplementA(ICpuRegistersService registers)
    {
        registers.A = (byte)~registers.A;
        registers.F.Subtract = true;
        registers.F.HalfCarry = true;
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Sets the InterruptMasterEnable to false
    /// </summary>
    /// Verified against BGB
    public void DisableInterrupts(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = false;
        registers.ProgramCounter += 1;
    }

    public void EnableInterrupts(ICpuRegistersService registers)
    {
        registers.InterruptMasterEnable = true;
        registers.InterruptsJustEnabled = true;
        registers.ProgramCounter += 1;
    }

    public void SetCarryFlag(ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.F.Carry = true;
        registers.ProgramCounter += 1;
    }

    public void FlipCarryFlag(ICpuRegistersService registers)
    {
        registers.F.Subtract = false;
        registers.F.HalfCarry = false;
        registers.F.Carry = !registers.F.Carry;
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Halt the CPU with the program counter one higher than the current instruction
    /// TODO: Implement exiting a halt state from interrupt or reset
    /// </summary>
    /// Verified against BGB
    public void Halt(ICpuRegistersService registers)
    {
        var interruptEnableRegister = mmuService.ReadByte(AddressConsts.INTERRUPT_ENABLE_REGISTER_ADDRESS);
        var interruptRequestRegister = mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        if (registers.InterruptMasterEnable || (interruptEnableRegister & interruptRequestRegister & 0x1f) == 0)
        {
            registers.Halted = true;
        }
        else
        {
            registers.HaltBug = true;
        }
    }

    /// <summary>
    /// Stop the the CPU with the program counter one higher than the current instruction
    /// TODO: Implement Actual stopping behaviour
    /// </summary>
    /// Verified against BGB
    public void Stop(ICpuRegistersService registers)
    {
        registers.ProgramCounter += 2;
        clockService.Clock();
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x00:
            case 0xCB:
            case 0xD3:
            case 0xDB:
            case 0xDD:
            case 0xE3:
            case 0xE4:
            case 0xEB:
            case 0xEC:
            case 0xED:
            case 0xF4:
            case 0xFC:
            case 0xFD:
                NOP(registers);
                break;
            case 0x10:
                Stop(registers);
                break;
            case 0x27:
                DAA(registers);
                break;
            case 0x2F:
                ComplementA(registers);
                break;
            case 0x37:
                SetCarryFlag(registers);
                break;
            case 0x3F:
                FlipCarryFlag(registers);
                break;
            case 0x76:
                Halt(registers);
                break;
            case 0xF3:
                DisableInterrupts(registers);
                break;
            case 0xFB:
                EnableInterrupts(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in MiscellaneousInstructions.");
        }
    }

}