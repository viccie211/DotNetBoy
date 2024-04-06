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
            { 0x40, LoadBIntoB },
            { 0x41, LoadCIntoB },
            { 0x42, LoadDIntoB },
            { 0x43, LoadEIntoB },
            { 0x44, LoadHIntoB },
            { 0x45, LoadLIntoB },
            //0x46 is in LoadInstructions since it doesn't load between registers
            { 0x47, LoadAIntoB },
            { 0x48, LoadBIntoC },
            { 0x49, LoadCIntoC },
            { 0x4A, LoadDIntoC },
            { 0x4B, LoadEIntoC },
            { 0x4C, LoadHIntoC },
            { 0x4D, LoadLIntoC },
            //0x4E is in LoadInstructions since it doesn't load between registers
            { 0x4F, LoadAIntoC },
            { 0x50, LoadBIntoD },
            { 0x51, LoadCIntoD },
            { 0x52, LoadDIntoD },
            { 0x53, LoadEIntoD },
            { 0x54, LoadHIntoD },
            { 0x55, LoadLIntoD },
            //0x56 is in LoadInstructions since it doesn't load between registers
            { 0x57, LoadAIntoD },
            { 0x58, LoadBIntoE },
            { 0x59, LoadCIntoE },
            { 0x5A, LoadDIntoE },
            { 0x5B, LoadEIntoE },
            { 0x5C, LoadHIntoE },
            { 0x5D, LoadLIntoE },
            //0x5E is in LoadInstructions since it doesn't load between registers
            { 0x5F, LoadAIntoE },
            { 0x60, LoadBIntoH },
            { 0x61, LoadCIntoH },
            { 0x62, LoadDIntoH },
            { 0x63, LoadEIntoH },
            { 0x64, LoadHIntoH },
            { 0x65, LoadLIntoH },
            //0x66 is in LoadInstructions since it doesn't load between registers
            { 0x67, LoadAIntoH },
            { 0x68, LoadBIntoL },
            { 0x69, LoadCIntoL },
            { 0x6A, LoadDIntoL },
            { 0x6B, LoadEIntoL },
            { 0x6C, LoadHIntoL },
            { 0x6D, LoadLIntoL },
            //0x6E is in LoadInstructions since it doesn't load between registers
            { 0x6F, LoadAIntoL },
            { 0x78, LoadBIntoA },
            { 0x79, LoadCIntoA },
            { 0x7A, LoadDIntoA },
            { 0x7B, LoadEIntoA },
            { 0x7C, LoadHIntoA },
            { 0x7D, LoadLIntoA },
            //0x7E is in LoadInstructions since it doesn't load between registers
            { 0x7F, LoadAIntoA },
            { 0xF9, LoadHLIntoStackPointer }
        };

        _clockService = clockService;
    }

    #region LoadIntoB

    /// <summary>
    /// Load the contents of register B into register B
    /// </summary>
    /// 
    public void LoadBIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register B
    /// </summary>
    /// 
    public void LoadCIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register B
    /// </summary>
    /// 
    public void LoadDIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register B
    /// </summary>
    /// 
    public void LoadEIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register B
    /// </summary>
    /// 
    public void LoadHIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register B
    /// </summary>
    /// 
    public void LoadLIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register B
    /// </summary>
    /// verified against BGB
    public void LoadAIntoB(ICpuRegistersService registers)
    {
        LoadByteIntoB(registers.A, registers);
    }

    #endregion

    #region LoadIntoC

    /// <summary>
    /// Load the contents of register B into register C
    /// </summary>
    /// 
    public void LoadBIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register C
    /// </summary>
    /// 
    public void LoadCIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register C
    /// </summary>
    /// 
    public void LoadDIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register C
    /// </summary>
    /// 
    public void LoadEIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register C
    /// </summary>
    /// 
    public void LoadHIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register C
    /// </summary>
    /// 
    public void LoadLIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register C
    /// </summary>
    /// verified against BGB
    public void LoadAIntoC(ICpuRegistersService registers)
    {
        LoadByteIntoC(registers.A, registers);
    }

    #endregion

    #region LoadIntoD

    /// <summary>
    /// Load the contents of register B into register D
    /// </summary>
    /// 
    public void LoadBIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register D
    /// </summary>
    /// 
    public void LoadCIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register D
    /// </summary>
    /// 
    public void LoadDIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register D
    /// </summary>
    /// 
    public void LoadEIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register D
    /// </summary>
    /// 
    public void LoadHIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register D
    /// </summary>
    /// 
    public void LoadLIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register D
    /// </summary>
    /// verified against BGB
    public void LoadAIntoD(ICpuRegistersService registers)
    {
        LoadByteIntoD(registers.A, registers);
    }

    #endregion

    #region LoadIntoE

    /// <summary>
    /// Load the contents of register B into register E
    /// </summary>
    /// 
    public void LoadBIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register E
    /// </summary>
    /// 
    public void LoadCIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register E
    /// </summary>
    /// 
    public void LoadDIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register E
    /// </summary>
    /// 
    public void LoadEIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register E
    /// </summary>
    /// 
    public void LoadHIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register E
    /// </summary>
    /// 
    public void LoadLIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register E
    /// </summary>
    /// verified against BGB
    public void LoadAIntoE(ICpuRegistersService registers)
    {
        LoadByteIntoE(registers.A, registers);
    }

    #endregion

    #region LoadIntoH

    /// <summary>
    /// Load the contents of register B into register H
    /// </summary>
    /// 
    public void LoadBIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register H
    /// </summary>
    /// 
    public void LoadCIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register H
    /// </summary>
    /// 
    public void LoadDIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register H
    /// </summary>
    /// 
    public void LoadEIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register H
    /// </summary>
    /// 
    public void LoadHIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register H
    /// </summary>
    /// 
    public void LoadLIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register H
    /// </summary>
    /// verified against BGB
    public void LoadAIntoH(ICpuRegistersService registers)
    {
        LoadByteIntoH(registers.A, registers);
    }

    #endregion

    #region LoadIntoL

    /// <summary>
    /// Load the contents of register B into register L
    /// </summary>
    /// 
    public void LoadBIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register L
    /// </summary>
    /// 
    public void LoadCIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register L
    /// </summary>
    /// 
    public void LoadDIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register L
    /// </summary>
    /// 
    public void LoadEIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register L
    /// </summary>
    /// 
    public void LoadHIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register L
    /// </summary>
    /// 
    public void LoadLIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register L
    /// </summary>
    /// verified against BGB
    public void LoadAIntoL(ICpuRegistersService registers)
    {
        LoadByteIntoL(registers.A, registers);
    }

    #endregion

    #region LoadIntoA

    /// <summary>
    /// Load the contents of register B into register A 
    /// </summary>
    /// 
    public void LoadBIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.B, registers);
    }

    /// <summary>
    /// Load the contents of register C into register A 
    /// </summary>
    /// 
    public void LoadCIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.C, registers);
    }

    /// <summary>
    /// Load the contents of register D into register A 
    /// </summary>
    /// 
    public void LoadDIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.D, registers);
    }

    /// <summary>
    /// Load the contents of register E into register A 
    /// </summary>
    /// 
    public void LoadEIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.E, registers);
    }

    /// <summary>
    /// Load the contents of register H into register A 
    /// </summary>
    /// 
    public void LoadHIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.H, registers);
    }

    /// <summary>
    /// Load the contents of register L into register A 
    /// </summary>
    /// Verified against BGB
    public void LoadLIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.L, registers);
    }

    /// <summary>
    /// Load the contents of register A into register A 
    /// </summary>
    /// 
    public void LoadAIntoA(ICpuRegistersService registers)
    {
        LoadByteIntoA(registers.A, registers);
    }

    #endregion

    public void LoadHLIntoStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = registers.HL;
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoA(byte toLoad, ICpuRegistersService registers)
    {
        registers.A = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoB(byte toLoad, ICpuRegistersService registers)
    {
        registers.B = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoC(byte toLoad, ICpuRegistersService registers)
    {
        registers.C = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoD(byte toLoad, ICpuRegistersService registers)
    {
        registers.D = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoE(byte toLoad, ICpuRegistersService registers)
    {
        registers.E = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoH(byte toLoad, ICpuRegistersService registers)
    {
        registers.H = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }

    private void LoadByteIntoL(byte toLoad, ICpuRegistersService registers)
    {
        registers.L = toLoad;
        _clockService.Clock();
        registers.ProgramCounter += 1;
    }
}