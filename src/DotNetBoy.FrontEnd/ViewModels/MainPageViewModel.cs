using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Emulator;

namespace DotNetBoy.FrontEnd.ViewModels
{
    public class MainPageViewModel
    {
        public readonly IPpuService _ppuService;
        public readonly Cpu _cpu;
        public readonly IMmuService _mmuService;
        public readonly ICpuRegistersService _cpuRegistersService;

        public MainPageViewModel(IPpuService ppuService, Cpu cpu, IMmuService mmuService,
            ICpuRegistersService cpuRegistersService)
        {
            _ppuService = ppuService;
            _mmuService = mmuService;
            _cpuRegistersService = cpuRegistersService;
            _cpu = cpu;
            _cpuRegistersService.Reset();
            // _mmuService.LoadRom(File.ReadAllBytes("D:\\Repos\\DotNetBoy\\src\\DotNetBoy\\DebugAssets\\bgbw64\\06-ld r,r.gb")); 
            _mmuService.LoadRom(File.ReadAllBytes("D:\\Repos\\DotNetBoy\\src\\DotNetBoy\\DebugAssets\\Tetris.gb"));
        }

        public async Task StartEmulation()
        {
            Task.Run(() => _cpu.Loop());
        }
    }
}