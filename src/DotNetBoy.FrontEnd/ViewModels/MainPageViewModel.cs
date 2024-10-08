using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Emulator;
using DotNetBoy.Resources;
using Microsoft.Extensions.Configuration;

namespace DotNetBoy.FrontEnd.ViewModels
{
    public class MainPageViewModel
    {
        public readonly IPpuService PpuService;
        public readonly Cpu Cpu;
        public readonly IMmuService MmuService;
        public readonly ICpuRegistersService CpuRegistersService;
        public readonly int Scale;

        public MainPageViewModel(IPpuService ppuService, Cpu cpu, IMmuService mmuService,
            ICpuRegistersService cpuRegistersService, IConfiguration configuration)
        {
            PpuService = ppuService;
            MmuService = mmuService;
            CpuRegistersService = cpuRegistersService;
            Cpu = cpu;
            CpuRegistersService.Reset();
            MmuService.LoadRom(Roms.GetRom(configuration["RomName"]!));
            Scale = configuration.GetValue<int>("Scale");
        }

        public async Task StartEmulation()
        {
            Task.Run(() => Cpu.Loop());
        }
    }
}