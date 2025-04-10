using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Resources;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDotNetBoy();
serviceCollection.AddScoped<ConsoleScreen>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var scope = serviceProvider.CreateScope();

var cpuRegisters = scope.ServiceProvider.GetService<ICpuRegistersService>()!;
cpuRegisters.Reset();

// var rom = Roms.InstructionsTestRom;
// var rom = Roms.AllInstructionTest;
var rom = File.ReadAllBytes(Roms.RomFileInfos.First(x => x.Name == "04-op r,imm.gb").FullName);
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var ppuService = scope.ServiceProvider.GetService<IPpuService>();
var cpu = scope.ServiceProvider.GetService<Cpu>()!;
var instructionSetService = scope.ServiceProvider.GetService<IInstructionSetService>()!;
var consoleScreen = scope.ServiceProvider.GetService<ConsoleScreen>();

cpu.Loop();