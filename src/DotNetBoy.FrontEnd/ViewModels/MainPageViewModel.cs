using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Emulator;
using Microsoft.Extensions.Configuration;

namespace DotNetBoy.FrontEnd.ViewModels
{
    public class MainPageViewModel
    {
        public readonly IPpuService _ppuService;
        public readonly Cpu _cpu;
        public readonly IMmuService _mmuService;
        public readonly ICpuRegistersService _cpuRegistersService;

        public MainPageViewModel(IPpuService ppuService, Cpu cpu, IMmuService mmuService,
            ICpuRegistersService cpuRegistersService, IConfiguration configuration)
        {
            _ppuService = ppuService;
            _mmuService = mmuService;
            _cpuRegistersService = cpuRegistersService;
            _cpu = cpu;
            _cpuRegistersService.Reset();
            _mmuService.LoadRom(File.ReadAllBytes(configuration["RomPath"]??""));
        }

        public async Task StartEmulation()
        {
            Task.Run(() => _cpu.Loop());
        }
    }
}