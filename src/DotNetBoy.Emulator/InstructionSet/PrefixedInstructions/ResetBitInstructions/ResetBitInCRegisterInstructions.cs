using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;

public class ResetBitInCRegisterInstructions : ResetBitInstructionsBase, IInstructionSet
{
    private readonly IClockService _clockService;

    public Dictionary<byte, Action<ICpuRegistersService>> Instructions { get; }

    public ResetBitInCRegisterInstructions(IClockService clockService)
    {
        _clockService = clockService;

        Instructions = new Dictionary<byte, Action<ICpuRegistersService>>()
        {
            {
                0x91, ResetBit2InCRegister
            }
        };
    }

    public void ResetBit2InCRegister(ICpuRegistersService registers)
    {
        var mask = GetResetMask(2);
        registers.C = (byte)(registers.C & mask);
        _clockService.Clock(2);
        registers.ProgramCounter += 2;
    }
}