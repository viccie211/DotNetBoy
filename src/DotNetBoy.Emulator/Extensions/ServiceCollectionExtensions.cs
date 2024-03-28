using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.Emulator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmulator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IByteUshortService, ByteUshortService>();
        serviceCollection.AddScoped<IMmuService, MmuService>();
        serviceCollection.AddScoped<IClockService, ClockService>();
        serviceCollection.AddScoped<ITileService, TileService>();
        serviceCollection.AddScoped<IPpuService, PpuService>();
        serviceCollection.AddScoped<ICpuRegistersService, CpuRegistersService>();
        serviceCollection.AddScoped<JumpInstructions>();
        serviceCollection.AddScoped<MiscellaneousInstructions>();
        serviceCollection.AddScoped<LoadInstructions>();
        serviceCollection.AddScoped<LoadBetweenRegistersInstructions>();
        serviceCollection.AddScoped<IncrementInstructions>();
        serviceCollection.AddScoped<DecrementInstructions>();
        serviceCollection.AddScoped<LogicInstructions>();
        serviceCollection.AddScoped<StoreInstructions>();
        serviceCollection.AddScoped<RotateAndShiftInstructions>();
        serviceCollection.AddScoped<PushPopInstructions>();
        serviceCollection.AddScoped<ArithmeticInstructions>();
        serviceCollection.AddScoped<ShiftRightInstructions>();
        serviceCollection.AddScoped<ResetBitInBRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInCRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInDRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInERegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInHRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInLRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitAtAddressHLInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<RotateRightInstructions>();
        serviceCollection.AddScoped<SwapInstructions>();

        serviceCollection.AddScoped<IInstructionSetService, InstructionSetService>();

        serviceCollection.AddScoped<Cpu>();
        return serviceCollection;
    }
}