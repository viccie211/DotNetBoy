// See https://aka.ms/new-console-template for more information

using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Resources;
using Microsoft.Extensions.DependencyInjection;
using SpecBoy;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDotNetBoy();
serviceCollection.AddScoped<ReferenceEmulator>();
var serviceProvider = serviceCollection.BuildServiceProvider();
var scope = serviceProvider.CreateScope();

var cpuRegisters = scope.ServiceProvider.GetService<ICpuRegistersService>()!;
cpuRegisters.Reset();

var romFileName = Roms.RomFileInfos.First(x => x.Name == "Super Mario Land.gb").FullName;
var rom = File.ReadAllBytes(romFileName);
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var ppuService = scope.ServiceProvider.GetService<IPpuService>()!;
var cpu = scope.ServiceProvider.GetService<DotNetBoy.Emulator.Cpu>()!;
var instructionSetService = scope.ServiceProvider.GetService<IInstructionSetService>()!;
var timerService = scope.ServiceProvider.GetService<ITimerService>()!;

var referenceEmulator = scope.ServiceProvider.GetService<ReferenceEmulator>()!;
referenceEmulator.LoadRom(romFileName);


var dotNetBoyStatus = "";
var referenceEmulatorStatus = "";
var originalPc = cpuRegisters.ProgramCounter;
while (dotNetBoyStatus == referenceEmulatorStatus)
{
    originalPc = cpuRegisters.ProgramCounter;
    cpu.Step();
    referenceEmulator.cpu.Execute();
    if (!cpu.Prefixed && cpu.Instruction == 0xF0 && mmuService.ReadByte((ushort)(originalPc + 1)) == 0x44)
    {
        cpuRegisters.A = referenceEmulator.cpu.A;
    }

    dotNetBoyStatus =
        $"{cpuRegisters} 0x{(cpu.Prefixed ? DotNetBoy.Emulator.Cpu.INSTRUCTION_PREFIX.ToString("x2") : "")}{cpu.Instruction.ToString("x2")} DivCounter: {timerService.DivCounter} TIMA:{mmuService.ReadByte(AddressConsts.TIMA_REGISTER)}";
    referenceEmulatorStatus =
        $"{referenceEmulator.cpu} 0x{(referenceEmulator.cpu.Prefixed ? DotNetBoy.Emulator.Cpu.INSTRUCTION_PREFIX.ToString("x2") : "")}{referenceEmulator.cpu.Instruction.ToString("x2")} DivCounter: {referenceEmulator.timers.divCounter} TIMA:{referenceEmulator.mem.ReadByte(AddressConsts.TIMA_REGISTER)}";
    Console.WriteLine($"DotNetBoy:\t\t{dotNetBoyStatus}");
    Console.WriteLine($"ReferenceEmulator:\t{referenceEmulatorStatus}");
}

if (cpu.Instruction == 0xf0)
{
    var lastpartofaddress = mmuService.ReadByte((ushort)(originalPc + 1));
    var result = mmuService.ReadByte((ushort)(0xff00 + lastpartofaddress));
    Console.WriteLine($"Read 0xff{lastpartofaddress:x2} got {result:x2}");
}
if (cpu.Instruction == 0xf2)
{
    var lastpartofaddress = cpuRegisters.C;
    var result = mmuService.ReadByte((ushort)(0xff00 + lastpartofaddress));
    Console.WriteLine($"Read 0xff{lastpartofaddress:x2} got {result:x2}");
}