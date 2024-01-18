using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.Interfaces;
using DotNetBoy.Emulator.Services.Implementations;

var byteUshortService = new ByteUshortService();
var mmuService = new MmuService(byteUshortService);
var cpuRegisters = new CpuRegisters(byteUshortService);
var nonPrefixedInstructions = new List<IInstructionSet>()
{
    new JumpInstructions(mmuService),
    new MiscellaneousInstructions()
};

var prefixedInstructions = new List<IInstructionSet>()
{
};
var cpu = new Cpu(byteUshortService, mmuService, cpuRegisters, nonPrefixedInstructions, prefixedInstructions);
cpuRegisters.Reset();
var rom = Roms.BgbTestRom;
mmuService.LoadRom(rom);
cpu.Loop();