using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class LogicInstructions : IInstructionSet
{
    private readonly IMmuService _mmuService;

    public LogicInstructions(IMmuService mmuService)
    {
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0xFE, CompareAToD8 },
            { 0xAF, XORAWithA }
        };
    }

    private void CompareAToD8(CpuRegisters cpu)
    {
        var d8 = _mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1));
        var result = (byte)(cpu.A - d8);
        cpu.F.Subtract = true;
        cpu.F.Zero = result == 0;
        cpu.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(cpu.A, d8);
        cpu.F.Carry = InstructionUtilFunctions.CarryFor8BitSubtract(cpu.A, d8);
        cpu.Clock(2);
        cpu.ProgramCounter += 2;
    }

    private void XORAWithA(CpuRegisters cpu)
    {
        cpu.A = (byte)(cpu.A ^ cpu.A);
        cpu.F.Zero = cpu.A == 0;
        cpu.F.Subtract = false;
        cpu.F.Carry = false;
        cpu.F.HalfCarry = false;
        cpu.Clock();
        cpu.ProgramCounter += 1;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}