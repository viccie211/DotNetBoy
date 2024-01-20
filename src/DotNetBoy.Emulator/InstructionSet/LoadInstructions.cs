using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LoadInstructions :IInstructionSet
{
    
    private readonly IMmuService _mmuService;
    private readonly IClockService _clockService;
    public LoadInstructions(IMmuService mmuService, IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x01, LoadD16IntoBC },
            { 0x02, LoadAtAddressBCIntoA },
            { 0x06, LoadD8IntoB },
            {0x31,LoadD16IntoStackPointer},
            { 0x0F0, LoadAtAddressFF00PlusD8IntoA }
        };
        _mmuService = mmuService;
        _clockService = clockService;
    }
    /// <summary>
    /// Load a word from memory into the BC ister
    /// </summary>
    private void LoadD16IntoBC(CpuRegisters cpu)
    {
        cpu.BC = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _clockService.Clock(3);
        cpu.ProgramCounter += 3;
    }
    
    private void LoadD16IntoStackPointer(CpuRegisters cpu)
    {
        cpu.StackPointer = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _clockService.Clock(3);
        cpu.ProgramCounter += 3;
    }
    
    // /// <summary>
    // /// Load a byte at the address in the BC ister into the A ister
    // /// </summary>
    private void LoadAtAddressBCIntoA(CpuRegisters cpu)
    {
        cpu.A = _mmuService.ReadByte(cpu.BC);
        _clockService.Clock(2);
        cpu.ProgramCounter += 1;
    }
    
    private void LoadD8IntoB(CpuRegisters cpu)
    {
        cpu.B = _mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1));
        _clockService.Clock(2);
        cpu.ProgramCounter += 2;
    }
    
    /// <summary>
    /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    private void LoadAtAddressFF00PlusD8IntoA(CpuRegisters cpu)
    {
        var address = (ushort)(0xFF00 + _mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1)));
        cpu.A = _mmuService.ReadByte(address);
        _clockService.Clock(3);
        cpu.ProgramCounter += 2;
    }
    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}