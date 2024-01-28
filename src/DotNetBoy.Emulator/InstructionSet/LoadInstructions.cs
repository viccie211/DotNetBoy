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
            { 0x11, LoadD16IntoDE },
            { 0x21, LoadD16IntoHL },
            { 0x7B, LoadEIntoA },
            { 0x31, LoadD16IntoStackPointer },
            { 0xF0, LoadAtAddressFF00PlusD8IntoA },
        };
        _mmuService = mmuService;
        _clockService = clockService;
    }

    /// <summary>
    /// Load a word from memory into the BC ister
    /// </summary>
    public void LoadD16IntoBC(ICpuRegistersService registers)
    {
        registers.BC = LoadD16(registers);
    }

    /// <summary>
    /// Load the next word in memory into the Stack pointer
    /// </summary>
    public void LoadD16IntoStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = LoadD16(registers);
    }

    /// <summary>
    /// Load the next word in memory into the HL Register
    /// </summary>
    public void LoadD16IntoHL(ICpuRegistersService registers)
    {
        registers.HL = LoadD16(registers);
    }

    /// <summary>
    /// Load the next word in memory into the HL register
    /// </summary>
    public void LoadD16IntoDE(ICpuRegistersService registers)
    {
        registers.DE = LoadD16(registers);
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
    /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void LoadAtAddressFF00PlusD8IntoA(ICpuRegistersService registers)
    {
        var address = (ushort)(0xFF00 + _mmuService.ReadByte((ushort)(registers.ProgramCounter + 1)));
        registers.A = _mmuService.ReadByte(address);
        _clockService.Clock(3);
        registers.ProgramCounter += 2;
    }

    public void LoadEIntoA(ICpuRegistersService registers)
    {
        registers.A = registers.E;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private ushort LoadD16(ICpuRegistersService registers)
    {
        var result = _mmuService.ReadWordLittleEndian((ushort)(registers.ProgramCounter + 1));
        _clockService.Clock(3);
        registers.ProgramCounter += 3;
        return result;
    }

    private byte LoadD8(ICpuRegistersService registers)
    {
        var result = _mmuService.ReadByte((ushort)(registers.ProgramCounter + 1));
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
        return result;
    }
}