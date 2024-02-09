using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class PushPopInstructions : IInstructionSet
{
    private readonly IClockService _clockService;
    private readonly IMmuService _mmuService;
    private readonly IByteUshortService _byteUshortService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public PushPopInstructions(IClockService clockService, IMmuService mmuService, IByteUshortService byteUshortService)
    {
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>
        {
            { 0xC1, PopBC },
            { 0xC5, PushBC },
            { 0xD5, PushDE },
            { 0xE1, PopHL },
            { 0xE5, PushHL },
            { 0xF1, PopAF },
            { 0xF5, PushAF },
        };
        _clockService = clockService;
        _mmuService = mmuService;
        _byteUshortService = byteUshortService;
    }

    /// <summary>
    /// Pop the first word of the stack and store it in the BC register
    /// </summary>
    /// Verified against BGB
    public void PopBC(ICpuRegistersService registers)
    {
        registers.BC = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the BC register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushBC(ICpuRegistersService registers)
    {
        PushWord(registers.BC, registers);
    }

    /// <summary>
    /// Push the contents of the DE register to the stack
    /// </summary>
    /// 
    public void PushDE(ICpuRegistersService registers)
    {
        PushWord(registers.DE, registers);
    }

    /// <summary>
    /// Pop the first word of the stack and store it in the HL register
    /// </summary>
    /// Verified against BGB
    public void PopHL(ICpuRegistersService registers)
    {
        registers.HL = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the HL register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushHL(ICpuRegistersService registers)
    {
        PushWord(registers.HL, registers);
    }

    /// <summary>
    /// Pop the first word of the stack and store it in the AF register
    /// </summary>
    /// Verified against BGB
    public void PopAF(ICpuRegistersService registers)
    {
        registers.AF = PopWord(registers);
    }

    /// <summary>
    /// Push the contents of the AF register to the stack
    /// </summary>
    /// Verified against BGB
    public void PushAF(ICpuRegistersService registers)
    {
        PushWord(registers.AF, registers);
    }

    private void PushWord(ushort wordToPush, ICpuRegistersService registers)
    {
        var upper = _byteUshortService.UpperByteOfSixteenBits(wordToPush);
        var lower = _byteUshortService.LowerByteOfSixteenBits(wordToPush);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, upper);
        registers.StackPointer--;
        _mmuService.WriteByte(registers.StackPointer, lower);
        registers.ProgramCounter += 1;
        _clockService.Clock(4);
    }

    private ushort PopWord(ICpuRegistersService registers)
    {
        var lower = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        var upper = _mmuService.ReadByte(registers.StackPointer);
        registers.StackPointer++;
        registers.ProgramCounter += 1;
        _clockService.Clock(3);
        return _byteUshortService.CombineBytes(upper, lower);
    }
}