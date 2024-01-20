using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<IByteUshortService, ByteUshortService>();
serviceCollection.AddScoped<IMmuService, MmuService>();
serviceCollection.AddScoped<IClockService,ClockService>();
serviceCollection.AddScoped<IPpuService,PpuService>();
serviceCollection.AddScoped<CpuRegisters>();
serviceCollection.AddScoped<JumpInstructions>();
serviceCollection.AddScoped<MiscellaneousInstructions>();
serviceCollection.AddScoped<LoadInstructions>();
serviceCollection.AddScoped<IncrementInstructions>();
serviceCollection.AddScoped<DecrementInstructions>();
serviceCollection.AddScoped<LogicInstructions>();
serviceCollection.AddScoped<StoreInstructions>();
serviceCollection.AddScoped<RotateAndShiftInstructions>();
serviceCollection.AddScoped<IInstructionSetService, InstructionSetService>();
serviceCollection.AddScoped<Cpu>();

var serviceProvider = serviceCollection.BuildServiceProvider();
var scope = serviceProvider.CreateScope();

var cpuRegisters = scope.ServiceProvider.GetService<CpuRegisters>()!;
cpuRegisters.Reset();

var rom = Roms.BgbTestRom;
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var ppuService = scope.ServiceProvider.GetService<IPpuService>();
var cpu = scope.ServiceProvider.GetService<Cpu>()!;
cpu.Loop();