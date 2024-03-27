using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddEmulator();
serviceCollection.AddScoped<ConsoleScreen>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var scope = serviceProvider.CreateScope();

var cpuRegisters = scope.ServiceProvider.GetService<ICpuRegistersService>()!;
cpuRegisters.Reset();

var rom = Roms.AllInstructionTest;
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var ppuService = scope.ServiceProvider.GetService<IPpuService>();
var cpu = scope.ServiceProvider.GetService<Cpu>()!;
var instructionSetService = scope.ServiceProvider.GetService<IInstructionSetService>()!;
var consoleScreen = scope.ServiceProvider.GetService<ConsoleScreen>();
Console.WriteLine(
    $"Implemented {instructionSetService.NonPrefixedInstructions.Count(x => x != null)} non prefixed instructions and {instructionSetService.PrefixedInstructions.Count(x => x != null)} prefixed instructions");
cpu.Loop();