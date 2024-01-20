using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceServiceCollection = new ServiceCollection();
serviceServiceCollection.AddScoped<IByteUshortService, ByteUshortService>();
serviceServiceCollection.AddScoped<IMmuService, MmuService>();
serviceServiceCollection.AddScoped<CpuRegisters>();
serviceServiceCollection.AddScoped<JumpInstructions>();
serviceServiceCollection.AddScoped<MiscellaneousInstructions>();
serviceServiceCollection.AddScoped<LoadInstructions>();
serviceServiceCollection.AddScoped<IncrementInstructions>();
serviceServiceCollection.AddScoped<DecrementInstructions>();
serviceServiceCollection.AddScoped<LogicInstructions>();
serviceServiceCollection.AddScoped<StoreInstructions>();
serviceServiceCollection.AddScoped<RotateAndShiftInstructions>();
serviceServiceCollection.AddScoped<IInstructionSetService, InstructionSetService>();
serviceServiceCollection.AddScoped<Cpu>();

var serviceProvider = serviceServiceCollection.BuildServiceProvider();
var scope = serviceProvider.CreateScope();

var cpuRegisters = scope.ServiceProvider.GetService<CpuRegisters>()!;
cpuRegisters.Reset();

var rom = Roms.BgbTestRom;
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var cpu = scope.ServiceProvider.GetService<Cpu>()!;
cpu.Loop();