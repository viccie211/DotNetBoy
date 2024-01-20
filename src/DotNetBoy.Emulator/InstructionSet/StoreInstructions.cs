using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet;

public class StoreInstructions : IInstructionSet
{
    private readonly IByteUshortService _byteUshortService;
    private readonly IMmuService _mmuService;

    public StoreInstructions(IByteUshortService byteUshortService,IMmuService mmuService)
    {
        _byteUshortService = byteUshortService;
        _mmuService = mmuService;
        Instructions = new Dictionary<byte, Action<CpuRegisters>>()
        {
            { 0x08, StoreStackPointerAtAddress }
        };
    }

    public void StoreStackPointerAtAddress(CpuRegisters cpu)
    {
        var lower = _byteUshortService.LowerByteOfSixteenBits(cpu.StackPointer);
        var upper = _byteUshortService.UpperByteOfSixteenBits(cpu.StackPointer);
        var targetAddress = _mmuService.ReadWordLittleEndian((ushort)(cpu.ProgramCounter + 1));
        _mmuService.WriteByte(targetAddress,lower);
        _mmuService.WriteByte((ushort)(targetAddress+1),upper);
        cpu.Clock(5);
        cpu.ProgramCounter += 3;
    }

    public Dictionary<byte, Action<CpuRegisters>> Instructions { get; }
}