using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.Emulator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDotNetBoy(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IByteUshortService, ByteUshortService>();
        serviceCollection.AddScoped<ITimerService, TimerService>();
        serviceCollection.AddScoped<IMmuService, MmuService>();
        serviceCollection.AddScoped<IClockService, ClockService>();
        serviceCollection.AddScoped<ITileService, TileService>();
        serviceCollection.AddScoped<IPpuService, PpuService>();
        serviceCollection.AddScoped<ICpuRegistersService, CpuRegistersService>();
        serviceCollection.AddScoped<RotateInstructions>();
        serviceCollection.AddScoped<JumpInstructions>();
        serviceCollection.AddScoped<MiscellaneousInstructions>();
        serviceCollection.AddScoped<LoadInstructions>();
        serviceCollection.AddScoped<LoadBetweenRegistersInstructions>();
        serviceCollection.AddScoped<IncrementInstructions>();
        serviceCollection.AddScoped<DecrementInstructions>();
        serviceCollection.AddScoped<LogicInstructions>();
        serviceCollection.AddScoped<StoreInstructions>();
        serviceCollection.AddScoped<PushPopInstructions>();
        serviceCollection.AddScoped<ArithmeticInstructions>();
        serviceCollection.AddScoped<ShiftRightLogicalInstructions>();
        serviceCollection.AddScoped<ShiftRightArithmeticInstructions>();
        serviceCollection.AddScoped<ShiftLeftArithmeticInstructions>();
        serviceCollection.AddScoped<BitInBRegisterInstructions>();
        serviceCollection.AddScoped<BitInCRegisterInstructions>();
        serviceCollection.AddScoped<BitInDRegisterInstructions>();
        serviceCollection.AddScoped<BitInERegisterInstructions>();
        serviceCollection.AddScoped<BitInHRegisterInstructions>();
        serviceCollection.AddScoped<BitInLRegisterInstructions>();
        serviceCollection.AddScoped<BitAtAddressHLInstructions>();
        serviceCollection.AddScoped<BitInARegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInBRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInCRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInDRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInERegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInHRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInLRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitAtAddressHLInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<SetBitInBRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInCRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInDRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInERegisterInstructions>();
        serviceCollection.AddScoped<SetBitInHRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInLRegisterInstructions>();
        serviceCollection.AddScoped<SetBitAtAddressHLInstructions>();
        serviceCollection.AddScoped<SetBitInARegisterInstructions>();
        serviceCollection.AddScoped<SetBitInARegisterInstructions>();
        serviceCollection.AddScoped<RotateRightInstructions>();
        serviceCollection.AddScoped<RotateLeftInstructions>();
        serviceCollection.AddScoped<RotateRightThroughCarryInstructions>();
        serviceCollection.AddScoped<RotateLeftThroughCarryInstructions>();
        serviceCollection.AddScoped<SwapInstructions>();
        serviceCollection.AddScoped<IEventService, EventService>();

        serviceCollection.AddScoped<IInstructionSetService, InstructionSetService>();

        serviceCollection.AddScoped<Cpu>();
        return serviceCollection;
    }
}