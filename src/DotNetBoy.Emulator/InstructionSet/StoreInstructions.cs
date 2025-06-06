﻿using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class StoreInstructions : IInstructionSet
{
    private readonly IByteUshortService _byteUshortService;
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;

    public StoreInstructions(IByteUshortService byteUshortService, IMmuService mmuService, IClockService clockService)
    {
        _byteUshortService = byteUshortService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x08, StoreStackPointerAtAddressD16 },
            { 0x02, StoreAtAddressBCFromA },
            { 0x12, StoreAtAddressDEFromA },
            { 0x22, StoreAtAddressHLFromAIncrementHL },
            { 0x32, StoreAtAddressHLFromADecrementHL },
            { 0x36, StoreAtAddressHLFromD8 },
            { 0x70, StoreAtAddressHLFromB },
            { 0x71, StoreAtAddressHLFromC },
            { 0x72, StoreAtAddressHLFromD },
            { 0x73, StoreAtAddressHLFromE },
            { 0x74, StoreAtAddressHLFromH },
            { 0x75, StoreAtAddressHLFromL },
            { 0x77, StoreAtAddressHLFromA },
            { 0xE0, StoreAtAddressFF00PlusD8FromA },
            { 0xE2, StoreAtAddressFF00PlusCFromA },
            { 0xEA, StoreAtA16FromA }
        };
        _clockService = clockService;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public void StoreStackPointerAtAddressD16(ICpuRegistersService registers)
    {
        var lower = _byteUshortService.LowerByteOfSixteenBits(registers.StackPointer);
        var upper = _byteUshortService.UpperByteOfSixteenBits(registers.StackPointer);
        var targetAddress =
            _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _mmuService.WriteByte(targetAddress, lower);
        _mmuService.WriteByte((ushort)(targetAddress + 1), upper);
        _clockService.Clock(4);
        registers.ProgramCounter += 3;
    }

    /// <summary>
    /// Store the contents of the B register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromB(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.B, registers);
    }

    /// <summary>
    /// Store the contents of the C register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromC(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.C, registers);
    }

    /// <summary>
    /// Store the contents of the D register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromD(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.D, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromE(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.E, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromH(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.H, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromL(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.L, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromA(ICpuRegistersService registers)
    {
        StoreByteAtAddressHL(registers.A, registers);
    }

    public void StoreAtAddressHLFromD8(ICpuRegistersService registers)
    {
        var toStore = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        StoreByteAtAddressHL(toStore, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the DE register
    /// </summary>
    public void StoreAtAddressBCFromA(ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.BC, registers.A);
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }


    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the DE register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressDEFromA(ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.DE, registers.A);
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// and afterwards increment the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromAIncrementHL(ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.HL, registers.A);
        registers.ProgramCounter += 1;
        registers.HL++;
        _clockService.Clock();
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the HL register
    /// and afterwards increment the HL register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressHLFromADecrementHL(ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.HL, registers.A);
        registers.ProgramCounter += 1;
        registers.HL--;
        _clockService.Clock();
    }

    /// <summary>
    /// Store from register A the to internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void StoreAtAddressFF00PlusD8FromA(ICpuRegistersService registers)
    {
        var address = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        registers.ProgramCounter += 1;
        StoreAtAddressFF00PlusByteFromA(address, registers);
    }

    /// <summary>
    /// Store from register A the to internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void StoreAtAddressFF00PlusCFromA(ICpuRegistersService registers)
    {
        StoreAtAddressFF00PlusByteFromA(registers.C, registers);
    }

    /// <summary>
    /// Store the contents of the A register at the address specified by the next word in memory
    /// </summary>
    /// Verified against BGB
    public void StoreAtA16FromA(ICpuRegistersService registers)
    {
        var address = _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock();
        _mmuService.WriteByte(address, registers.A);
        registers.ProgramCounter += 3;
        _clockService.Clock(2);
    }

    private void StoreByteAtAddressHL(byte toStore, ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.HL, toStore);
        registers.ProgramCounter += 1;
        _clockService.Clock();
    }

    private void StoreAtAddressFF00PlusByteFromA(byte relativeAddress, ICpuRegistersService registers)
    {
        var address = (ushort)(0xFF00 + relativeAddress);
        _mmuService.WriteByte(address, registers.A);
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }
    
    public void ExecuteInstruction(byte opCode, ICpuRegistersService registers)
    {
        switch (opCode)
        {
            case 0x08:
                StoreStackPointerAtAddressD16(registers);
                break;
            case 0x02:
                StoreAtAddressBCFromA(registers);
                break;
            case 0x12:
                StoreAtAddressDEFromA(registers);
                break;
            case 0x22:
                StoreAtAddressHLFromAIncrementHL(registers);
                break;
            case 0x32:
                StoreAtAddressHLFromADecrementHL(registers);
                break;
            case 0x36:
                StoreAtAddressHLFromD8(registers);
                break;
            case 0x70:
                StoreAtAddressHLFromB(registers);
                break;
            case 0x71:
                StoreAtAddressHLFromC(registers);
                break;
            case 0x72:
                StoreAtAddressHLFromD(registers);
                break;
            case 0x73:
                StoreAtAddressHLFromE(registers);
                break;
            case 0x74:
                StoreAtAddressHLFromH(registers);
                break;
            case 0x75:
                StoreAtAddressHLFromL(registers);
                break;
            case 0x77:
                StoreAtAddressHLFromA(registers);
                break;
            case 0xE0:
                StoreAtAddressFF00PlusD8FromA(registers);
                break;
            case 0xE2:
                StoreAtAddressFF00PlusCFromA(registers);
                break;
            case 0xEA:
                StoreAtA16FromA(registers);
                break;
            default:
                throw new NotImplementedException($"Opcode 0x{opCode:X2} not implemented in StoreInstructions.");
        }
    }

}