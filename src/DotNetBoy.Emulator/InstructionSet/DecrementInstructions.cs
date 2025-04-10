using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class DecrementInstructions : IInstructionSet
{
    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;

    public DecrementInstructions(IClockService clockService, IMmuService mmuService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x05, DecrementB },
            { 0x0B, DecrementBC },
            { 0x0D, DecrementC },
            { 0x15, DecrementD },
            { 0x1B, DecrementDE },
            { 0x1D, DecrementE },
            { 0x25, DecrementH },
            { 0x2B, DecrementHL },
            { 0x2D, DecrementL },
            { 0x35, DecrementAtAddressHL },
            { 0x3B, DecrementStackPointer },
            { 0x3D, DecrementA },
        };
        _clockService = clockService;
        _mmuService = mmuService;
    }

    /// <summary>
    /// Decrement the contents of the B register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementB(ICpuRegistersService registers)
    {
        registers.B = Decrement8Bits(registers.B, registers);
    }

    /// <summary>
    /// Decrement the contents of the C register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementC(ICpuRegistersService registers)
    {
        registers.C = Decrement8Bits(registers.C, registers);
    }

    /// <summary>
    /// Decrement the contents of the D register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementD(ICpuRegistersService registers)
    {
        registers.D = Decrement8Bits(registers.D, registers);
    }

    /// <summary>
    /// Decrement the contents of the E register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementE(ICpuRegistersService registers)
    {
        registers.E = Decrement8Bits(registers.E, registers);
    }

    /// <summary>
    /// Decrement the contents of the H register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementH(ICpuRegistersService registers)
    {
        registers.H = Decrement8Bits(registers.H, registers);
    }

    /// <summary>
    /// Decrement the contents of the L register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementL(ICpuRegistersService registers)
    {
        registers.L = Decrement8Bits(registers.L, registers);
    }

    /// <summary>
    /// Decrement the contents memory specified by the HL register register Z 1 H - 
    /// </summary>
    /// 
    public void DecrementAtAddressHL(ICpuRegistersService registers)
    {
        var toDecrement = _mmuService.ReadByte(registers.HL);
        _mmuService.WriteByte(registers.HL, Decrement8Bits(toDecrement, registers));
        _clockService.Clock(2);
    }

    /// <summary>
    /// Decrement the contents of the A register Z 1 H - 
    /// </summary>
    /// Verified against BGB 
    public void DecrementA(ICpuRegistersService registers)
    {
        registers.A = Decrement8Bits(registers.A, registers);
    }

    public void DecrementBC(ICpuRegistersService registers)
    {
        registers.BC = Decrement16Bits(registers.BC, registers);
    }

    public void DecrementDE(ICpuRegistersService registers)
    {
        registers.DE = Decrement16Bits(registers.DE, registers);
    }

    public void DecrementHL(ICpuRegistersService registers)
    {
        registers.HL = Decrement16Bits(registers.HL, registers);
    }

    public void DecrementStackPointer(ICpuRegistersService registers)
    {
        registers.StackPointer = Decrement16Bits(registers.StackPointer, registers);
    }

    private byte Decrement8Bits(byte initial, ICpuRegistersService registers)
    {
        registers.F.Subtract = true;
        registers.F.HalfCarry = InstructionUtilFunctions.HalfCarryFor8BitSubtraction(initial, 0x01);
        var result = (byte)(initial - 1);
        registers.F.Zero = result == 0;
        registers.ProgramCounter += 1;
        return result;
    }

    private ushort Decrement16Bits(ushort initial, ICpuRegistersService registers)
    {
        var result = (ushort)(initial - 1);
        _clockService.Clock(2);
        registers.ProgramCounter += 1;
        return result;
    }
}