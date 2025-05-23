using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.AvaloniaFrontend.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IServiceScope ServiceScope;
    private readonly Cpu Cpu;
    private readonly IEventService EventService;
    private readonly IMmuService MmuService;
    private readonly ICpuRegistersService CpuRegistersService;
    private readonly IPpuService PpuService;
    private readonly IJoyPadService JoyPadService;

    // This is the bitmap we’ll bind to the Image
    public WriteableBitmap FrameBitmap { get; }

    public MainViewModel()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDotNetBoy();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        ServiceScope = serviceProvider.CreateScope();
        MmuService = ServiceScope.ServiceProvider.GetRequiredService<IMmuService>();
        CpuRegistersService = ServiceScope.ServiceProvider.GetRequiredService<ICpuRegistersService>();
        EventService = ServiceScope.ServiceProvider.GetRequiredService<IEventService>();
        PpuService = ServiceScope.ServiceProvider.GetRequiredService<IPpuService>();
        JoyPadService = ServiceScope.ServiceProvider.GetRequiredService<IJoyPadService>();
        Cpu = ServiceScope.ServiceProvider.GetRequiredService<Cpu>();
        CpuRegistersService.Reset();
        MmuService.LoadRom(Roms.GetRom("Tetris.gb"));

        FrameBitmap = new WriteableBitmap(
            new PixelSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT),
            new Vector(96, 96), // DPI X/Y
            PixelFormat.Bgra8888,
            AlphaFormat.Premul);

        // subscribe to VBlankStart
        EventService.VBlankStart += OnVBlank;
        Task.Run(() =>
        {
            while (true)
            {
                Cpu.Step();
            }
        });
    }

    private void OnVBlank(object sender, VBlankEventArgs e)
    {
        Drawing = true;
        Dispatcher.UIThread.Post(() =>
        {
            using (var fb = FrameBitmap.Lock())
            {
                int w = ScreenDimensions.WIDTH;
                int h = ScreenDimensions.HEIGHT;
                int stride = fb.RowBytes;

                // allocate a managed buffer the size of the bitmap
                byte[] pixelData = new byte[stride * h];

                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int gray = e.FrameBuffer[y, x];
                        byte intensity = (byte)(255 - gray * 85); // 0=white, 3=black

                        int idx = y * stride + x * 4;
fra
                        pixelData[idx + 0] = intensity; // B
                        pixelData[idx + 1] = intensity; // G
                        pixelData[idx + 2] = intensity; // R
                        pixelData[idx + 3] = 255; // A
                    }
                }

                // copy to native buffer
                System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, fb.Address, pixelData.Length);
                Drawing = false;
            }
        });
    }

    public bool Drawing { get; set; }
}