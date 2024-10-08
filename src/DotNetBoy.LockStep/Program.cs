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

var romFileName = Roms.RomFileInfos.First(x => x.Name == "09-op r,r.gb").FullName;
var rom = File.ReadAllBytes(romFileName);
var mmuService = scope.ServiceProvider.GetService<IMmuService>()!;
mmuService.LoadRom(rom);

var ppuService = scope.ServiceProvider.GetService<IPpuService>()!;
var cpu = scope.ServiceProvider.GetService<DotNetBoy.Emulator.Cpu>()!;
var instructionSetService = scope.ServiceProvider.GetService<IInstructionSetService>()!;

var specBoy = scope.ServiceProvider.GetService<ReferenceEmulator>()!;
specBoy.LoadRom(romFileName);


var dotNetBoyStatus = "";
var specBoyStatus = "";
while (dotNetBoyStatus == specBoyStatus)
{
    var originalPc = cpuRegisters.ProgramCounter;
    cpu.Step();
    specBoy.cpu.Execute();
    if (!cpu.Prefixed && cpu.Instruction == 0xF0 && mmuService.ReadByte((ushort)(originalPc + 1)) == 0x44)
    {
        cpuRegisters.A = specBoy.cpu.A;
    }
    dotNetBoyStatus = $"{cpuRegisters} 0x{(cpu.Prefixed?DotNetBoy.Emulator.Cpu.INSTRUCTION_PREFIX.ToString("x2"):"")}{cpu.Instruction.ToString("x2")}";
    specBoyStatus = $"{specBoy.cpu} 0x{(specBoy.cpu.Prefixed?DotNetBoy.Emulator.Cpu.INSTRUCTION_PREFIX.ToString("x2"):"")}{specBoy.cpu.Instruction.ToString("x2")}";
    Console.WriteLine($"DotNetBoy:\t\t{dotNetBoyStatus}");
    Console.WriteLine($"ReferenceEmulator:\t{specBoyStatus}");
}
