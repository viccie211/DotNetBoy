using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LoadInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public LoadInstructions(IMmuService mmuService, IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x01, LoadD16IntoBC },
            { 0x02, LoadAtAddressBCIntoA },
            { 0x06, LoadD8IntoB },
            { 0x0E, LoadD8IntoC },
            { 0x11, LoadD16IntoDE },
            { 0x1A, LoadAtAddressDEIntoA },
            { 0x21, LoadD16IntoHL },
            { 0x2A, LoadAtAddressHLIntoAIncrementHL },
            { 0x3E, LoadD8IntoA },
            { 0x31, LoadD16IntoStackPointer },
            { 0x46, LoadAtAddressHLIntoB },
            { 0xF0, LoadAtAddressFF00PlusD8IntoA },
            { 0xFA, LoadAtAddressA16IntoA }
        };
        _mmuService = mmuService;
        _clockService = clockService;
    }

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
    public void LoadAtAddressDEIntoA(ICpuRegistersService registers)
    {
        registers.A = _mmuService.ReadByte(registers.DE);
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
    }

    // /// <summary>
    // /// Load a byte at the address in the BC register into the A register
    // /// </summary>
    public void LoadAtAddressBCIntoA(ICpuRegistersService registers)
    {
        registers.A = _mmuService.ReadByte(registers.BC);
        _clockService.Clock(2);
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
    /// Loads the byte located at the address in memory specified by the HL register into the A register and afterwards increment the HL register.
    /// </summary>
    /// Verified against BGB
    public void LoadAtAddressHLIntoAIncrementHL(ICpuRegistersService registers)
    {
        registers.A = _mmuService.ReadByte(registers.HL);
        registers.HL++;
        registers.ProgramCounter += 1;
        _clockService.Clock(2);
    }

    /// <summary>
    /// Loads the byte located at the address in memory specified by the HL register into the A register and afterwards decrement the HL register.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoADecrementHL(ICpuRegistersService registers)
    {
        registers.A = _mmuService.ReadByte(registers.HL);
        registers.HL--;
        registers.ProgramCounter += 1;
        _clockService.Clock(2);
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
    /// Load the 8-bit contents of memory specified by register pair HL into register B.
    /// </summary>
    /// 
    public void LoadAtAddressHLIntoB(ICpuRegistersService registers)
    {
        registers.B = _mmuService.ReadByte(registers.HL);
        registers.ProgramCounter += 1;
        _clockService.Clock(2);
    }

    /// <summary>
    /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void LoadAtAddressFF00PlusD8IntoA(ICpuRegistersService registers)
    {
        var address = (ushort)(0xFF00 + _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter)));
        registers.A = _mmuService.ReadByte(address);
        _clockService.Clock(3);
        registers.ProgramCounter += 2;
    }

    /// <summary>
    /// Loads the byte located at the address specified by the next word in memory into the A register
    /// </summary>
    public void LoadAtAddressA16IntoA(ICpuRegistersService registers)
    {
        var address = _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        registers.A = _mmuService.ReadByte(address);
        registers.ProgramCounter += 3;
        _clockService.Clock(4);
    }

    #region private methods

    private ushort LoadD16(ICpuRegistersService registers)
    {
        var result = _mmuService.ReadWordLittleEndian(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock(3);
        registers.ProgramCounter += 3;
        return result;
    }

    private byte LoadD8(ICpuRegistersService registers)
    {
        var result = _mmuService.ReadByte(InstructionUtilFunctions.NextAddress(registers.ProgramCounter));
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
        return result;
    }

    #endregion
}