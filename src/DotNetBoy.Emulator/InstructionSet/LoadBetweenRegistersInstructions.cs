using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;
public class LoadBetweenRegistersInstructions : IInstructionSet
{
    private readonly IClockService _clockService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public LoadBetweenRegistersInstructions(IClockService clockService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x42, LoadDIntoB },
            { 0x47, LoadAIntoB },
            { 0x78, LoadBIntoA },
            { 0x7B, LoadEIntoA },
            { 0x7C, LoadHIntoA },
            { 0x7D, LoadLIntoA },
        };

        _clockService = clockService;
    }
    /// <summary>
    /// Load the contents of the B register into the A register 
    /// </summary>
    /// 
    public void LoadBIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of the E register into the A register 
    /// </summary>
    /// 
    public void LoadEIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of the H register into the A register 
    /// </summary>
    /// 
    public void LoadHIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of the L register into the A register 
    /// </summary>
    /// Verified against BGB
    public void LoadLIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of the A Register into the B register
    /// </summary>
    /// verified against BGB
    public void LoadAIntoB(ICpuRegistersService registers)
    {
        registers.B = registers.A;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    /// <summary>
    /// Load the contents of register D in to register B
    /// </summary>
    /// 
    public void LoadDIntoB(ICpuRegistersService registers)
    {
        registers.B = registers.D;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoA(byte toLoad, ICpuRegistersService registers)
    {
        registers.A = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }
}