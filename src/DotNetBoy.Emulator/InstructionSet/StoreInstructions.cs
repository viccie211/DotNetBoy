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
        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            { 0x08, StoreStackPointerAtAddressD16 },
            { 0x12, StoreAtAddressDEFromA },
            { 0xE0, StoreAtAddressFF00PlusD8FromA }
        };
        _clockService = clockService;
    }

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public void StoreStackPointerAtAddressD16(ICpuRegistersService registers)
    {
        var lower = _byteUshortService.LowerByteOfSixteenBits(registers.StackPointer);
        var upper = _byteUshortService.UpperByteOfSixteenBits(registers.StackPointer);
        var targetAddress = _mmuService.ReadWordLittleEndian((ushort)(registers.ProgramCounter + 1));
        _mmuService.WriteByte(targetAddress, lower);
        _mmuService.WriteByte((ushort)(targetAddress + 1), upper);
        _clockService.Clock(5);
        registers.ProgramCounter += 3;
    }

    /// <summary>
    /// Store from register A the to internal RAM, port register, or mode register at the address in the range 0xFF00-0xFFFF specified by the next byte
    /// </summary>
    public void StoreAtAddressFF00PlusD8FromA(ICpuRegistersService registers)
    {
        var address = (ushort)(0xFF00 + _mmuService.ReadByte((ushort)(registers.ProgramCounter + 1)));
        _mmuService.WriteByte(address, registers.A);
        _clockService.Clock(3);
        registers.ProgramCounter += 2;
    }

    /// <summary>
    /// Store the contents of the A register at the address in memory specified by the DE register
    /// </summary>
    /// Verified against BGB
    public void StoreAtAddressDEFromA(ICpuRegistersService registers)
    {
        _mmuService.WriteByte(registers.DE, registers.A);
        registers.ProgramCounter += 1;
        _clockService.Clock(2);
    }
}