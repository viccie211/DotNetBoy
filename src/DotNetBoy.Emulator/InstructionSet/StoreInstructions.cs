using DotNetBoy.Emulator.InstructionSet.Interfaces;
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
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x08, StoreStackPointerAtAddress },
            { 0xE0, StoreAtAddressFF00PlusD8FromA}
        };
        _clockService = clockService;
    }
    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }

    private void StoreStackPointerAtAddress(CpuRegisters cpu)
    {
        var lower = _byteUshortService.LowerByteOfSixteenBits(cpu.StackPointer);
        var upper = _byteUshortService.UpperByteOfSixteenBits(cpu.StackPointer);
        var targetAddress = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _mmuService.WriteByte(targetAddress, lower);
        _mmuService.WriteByte((ushort)(targetAddress + 1), upper);
        _clockService.Clock(5);
        cpu.ProgramCounter += 3;
    }

    /// <summary>
    /// Load into register A the contents of the internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    private void StoreAtAddressFF00PlusD8FromA(CpuRegisters cpu)
    {
        var address = (ushort)(0xFF00 + _mmuService.ReadByte((ushort)(cpu.ProgramCounter + 1)));
        _mmuService.WriteByte(address, cpu.A);
        _clockService.Clock(3);
        cpu.ProgramCounter += 2;
    }
}