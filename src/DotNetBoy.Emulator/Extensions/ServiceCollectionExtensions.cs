﻿using DotNetBoy.Emulator.InstructionSet;
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
        serviceCollection.AddScoped<IClockService,ClockService>();
        serviceCollection.AddScoped<IPpuService,PpuService>();
        serviceCollection.AddScoped<ICpuRegistersService,CpuRegistersService>();
        serviceCollection.AddScoped<JumpInstructions>();
        serviceCollection.AddScoped<MiscellaneousInstructions>();
        serviceCollection.AddScoped<LoadInstructions>();
        serviceCollection.AddScoped<IncrementInstructions>();
        serviceCollection.AddScoped<DecrementInstructions>();
        serviceCollection.AddScoped<LogicInstructions>();
        serviceCollection.AddScoped<StoreInstructions>();
        serviceCollection.AddScoped<RotateAndShiftInstructions>();
        serviceCollection.AddScoped<PushPopInstructions>();
        serviceCollection.AddScoped<IInstructionSetService, InstructionSetService>();
        serviceCollection.AddScoped<Cpu>();
        return serviceCollection;
    }
}