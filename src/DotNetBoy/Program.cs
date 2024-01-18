using DotNetBoy;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Services.Implementations;

var byteUshortService = new ByteUshortService();
var mmuService = new MmuService(byteUshortService);
var cpu = new Cpu(byteUshortService,mmuService);
cpu.Reset();
var rom = Roms.BgbTestRom;
mmuService.LoadRom(rom);
cpu.Loop();
