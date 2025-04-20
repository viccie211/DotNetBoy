using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LoadInstructions(IMmuService mmuService, IClockService clockService) : IInstructionSet
{
    /// <summary>
    /// Load the next word from memory into the BC register
    /// </summary>
    /// Verified against BGB
    public void LoadD16IntoBC(ICpuRegistersService registers)
    {
        registers.BC = LoadD16(registers);
    }

    /// <summary>
    /// Load a byte at the address in the BC register into the A register
    /// </summary>
    /// Verified against BGB
    public void LoadD16IntoStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = LoadD16(registers);
    }

    /// <summary>
    /// Load the next word in memory into the HL Register
    /// </summary>
    /// Verified against BGB
    public void LoadD16IntoHL(ICpuRegistersService registers)
    {
        registers.HL = LoadD16(registers);
    }

    /// <summary>
    /// Load the next word in memory into the DE register
    /// </summary>
    /// Verified against BGB
    public void LoadD16IntoDE(ICpuRegistersService registers)
    {
        registers.DE = LoadD16(registers);
    }

    /// <summary>
    /// Load a byte at the address in the BC register into the A register
    /// </summary>
    /// Verified with BGB
    public void LoadAtAddressBCIntoA(ICpuRegistersService registers)
    {
        registers.A = mmuService.ReadByte(registers.BC);
        clockService.Clock();
        registers.ProgramCounter += 1;
    }


    /// <summary>
    /// Load a byte at the address in the DE register into the A register
    /// </summary>
    /// Verified with BGB
    public void LoadAtAddressDEIntoA(ICpuRegistersService registers)
    {
        registers.A = mmuService.ReadByte(registers.DE);
        clockService.Clock();
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Load the next byte into the B register
    /// </summary>
    /// Verified with BGB
    public void LoadD8IntoB(ICpuRegistersService registers)
    {
        registers.B = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the C register
    /// </summary>
    /// Verified with BGB
    public void LoadD8IntoC(ICpuRegistersService registers)
    {
        registers.C = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the C register
    /// </summary>
    /// Verified with BGB
    public void LoadD8IntoD(ICpuRegistersService registers)
    {
        registers.D = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the E register
    /// </summary>
    /// Verified with BGB
    public void LoadD8IntoE(ICpuRegistersService registers)
    {
        registers.E = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the H register
    /// </summary>
    /// 
    public void LoadD8IntoH(ICpuRegistersService registers)
    {
        registers.H = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the H register
    /// </summary>
    /// 
    public void LoadD8IntoL(ICpuRegistersService registers)
    {
        registers.L = LoadD8(registers);
    }

    /// <summary>
    /// Load the next byte into the A register
    /// </summary>
    /// Verified with BGB
    public void LoadD8IntoA(ICpuRegistersService registers)
    {
        registers.A = LoadD8(registers);
    }

    /// <summary>
    /// Loads the byte located at the address in memory specified by the HL register into the A register and afterwards increment the HL register.
    /// </summary>
    /// Verified against BGB
    public void LoadAtAddressHLIntoAIncrementHL(ICpuRegistersService registers)
    {
        registers.A = mmuService.ReadByte(registers.HL);
        registers.HL++;
        registers.ProgramCounter += 1;
        clockService.Clock();
    }

    /// <summary>
    /// Loads the byte located at the address in memory specified by the HL register into the A register and afterwards decrement the HL register.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoADecrementHL(ICpuRegistersService registers)
    {
        registers.A = mmuService.ReadByte(registers.HL);
        registers.HL--;
        registers.ProgramCounter += 1;
        clockService.Clock();
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register B.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoB(ICpuRegistersService registers)
    {
        registers.B = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register C.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoC(ICpuRegistersService registers)
    {
        registers.C = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register D.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoD(ICpuRegistersService registers)
    {
        registers.D = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register E.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoE(ICpuRegistersService registers)
    {
        registers.E = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register H.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoH(ICpuRegistersService registers)
    {
        registers.H = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register L.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoL(ICpuRegistersService registers)
    {
        registers.L = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load the 8-bit contents of memory specified by register pair HL into register A.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoA(ICpuRegistersService registers)
    {
        registers.A = LoadByteAtAddressHL(registers);
    }

    /// <summary>
    /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void LoadAtAddressFF00PlusD8IntoA(ICpuRegistersService registers)
    {
        var address =
            (ushort)(0xFF00 + mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter)));
        clockService.Clock();
        registers.A = mmuService.ReadByte(address);
        clockService.Clock();
        registers.ProgramCounter += 2;
    }

    public void LoadAtAddressFF00PlusCIntoA(ICpuRegistersService registers)
    {
        var address = (ushort)(0xFF00 + registers.C);
        registers.A = mmuService.ReadByte(address);
        clockService.Clock();
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Loads the byte located at the address specified by the next word in memory into the A register
    /// </summary>
    public void LoadAtAddressA16IntoA(ICpuRegistersService registers)
    {
        var address = mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        registers.A = mmuService.ReadByte(address);
        registers.ProgramCounter += 3;
        clockService.Clock(3);
    }

    public void LoadStackPointerPlusS8IntoHL(ICpuRegistersService registers)
    {
        var toAddUnsigned = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        var signedToAdd = InstructionUtilFunctions.ByteToSignedByte(toAddUnsigned);
        var resultInt = registers.StackPointer + signedToAdd;
        var result = (ushort)resultInt;
        registers.F.Zero = false;
        registers.F.Subtract = false;
        registers.F.HalfCarry = ((registers.StackPointer & 0xF) + (toAddUnsigned & 0xF)) > 0xF;
        registers.F.Carry = ((registers.StackPointer & 0xFF) + toAddUnsigned) > 0xFF;
        registers.HL = result;
        registers.ProgramCounter += 2;
        clockService.Clock(2);
    }

    #region private methods

    private ushort LoadD16(ICpuRegistersService registers)
    {
        var result = mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock(2);
        registers.ProgramCounter += 3;
        return result;
    }

    private byte LoadD8(ICpuRegistersService registers)
    {
        var result = mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        clockService.Clock();
        registers.ProgramCounter += 2;
        return result;
    }

    private byte LoadByteAtAddressHL(ICpuRegistersService registers)
    {
        var result = mmuService.ReadByte(registers.HL);
        registers.ProgramCounter += 1;
        clockService.Clock();
        return result;
    }

    #endregion

    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x01:
                LoadD16IntoBC(registers);
                break;
            case 0x06:
                LoadD8IntoB(registers);
                break;
            case 0x0E:
                LoadD8IntoC(registers);
                break;
            case 0x11:
                LoadD16IntoDE(registers);
                break;
            case 0x16:
                LoadD8IntoD(registers);
                break;
            case 0x0A:
                LoadAtAddressBCIntoA(registers);
                break;
            case 0x1A:
                LoadAtAddressDEIntoA(registers);
                break;
            case 0x1E:
                LoadD8IntoE(registers);
                break;
            case 0x21:
                LoadD16IntoHL(registers);
                break;
            case 0x26:
                LoadD8IntoH(registers);
                break;
            case 0x2A:
                LoadAtAddressHLIntoAIncrementHL(registers);
                break;
            case 0x3A:
                LoadAtAddressHLIntoADecrementHL(registers);
                break;
            case 0x2E:
                LoadD8IntoL(registers);
                break;
            case 0x3E:
                LoadD8IntoA(registers);
                break;
            case 0x31:
                LoadD16IntoStackPointer(registers);
                break;
            case 0x46:
                LoadAtAddressHLIntoB(registers);
                break;
            case 0x4E:
                LoadAtAddressHLIntoC(registers);
                break;
            case 0x56:
                LoadAtAddressHLIntoD(registers);
                break;
            case 0x5E:
                LoadAtAddressHLIntoE(registers);
                break;
            case 0x66:
                LoadAtAddressHLIntoH(registers);
                break;
            case 0x6E:
                LoadAtAddressHLIntoL(registers);
                break;
            case 0x7E:
                LoadAtAddressHLIntoA(registers);
                break;
            case 0xF0:
                LoadAtAddressFF00PlusD8IntoA(registers);
                break;
            case 0xF2:
                LoadAtAddressFF00PlusCIntoA(registers);
                break;
            case 0xF8:
                LoadStackPointerPlusS8IntoHL(registers);
                break;
            case 0xFA:
                LoadAtAddressA16IntoA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in LoadInstructions.");
        }
    }
}