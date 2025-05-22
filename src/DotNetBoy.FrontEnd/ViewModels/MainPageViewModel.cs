using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Emulator;
using DotNetBoy.Resources;
using Microsoft.Extensions.Configuration;

namespace DotNetBoy.FrontEnd.ViewModels
{
    public class MainPageViewModel
    {
        public readonly Cpu Cpu;
        public readonly IEventService EventService;
        public readonly IMmuService MmuService;
        public readonly ICpuRegistersService CpuRegistersService;
        public readonly IPpuService PpuService;
        public readonly IJoyPadService JoyPadService;
        public readonly int Scale;

        public MainPageViewModel(Cpu cpu, IMmuService mmuService,
            ICpuRegistersService cpuRegistersService, IConfiguration configuration, IEventService eventService, IPpuService ppuService, IJoyPadService joyPadService)
        {
            MmuService = mmuService;
            CpuRegistersService = cpuRegistersService;
            EventService = eventService;
            PpuService = ppuService;
            JoyPadService = joyPadService;
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