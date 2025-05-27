using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;
using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.AvaloniaFrontend.ViewModels;

public partial class MainViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly IServiceScope ServiceScope;
    private readonly Cpu Cpu;
    private readonly IEventService EventService;
    private readonly IMmuService MmuService;
    private readonly ICpuRegistersService CpuRegistersService;
    private readonly IPpuService PpuService;
    public readonly IJoyPadService JoyPadService;
    public event PropertyChangedEventHandler PropertyChanged;
    public WriteableBitmap FrameBitmap { get; set; }
    public string FrameCounterString { get; set; } = "";
    private int FrameCounter { get; set; }
    private readonly ManualResetEventSlim _vblankEvent = new();
    private const double TARGET_FRAME_TIME_MS = 16.67;
    private DateTime _lastFrameTime = DateTime.UtcNow;

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
        MmuService.LoadRom(Roms.GetRom("Super Mario Land.gb"));


        FrameBitmap = new WriteableBitmap(
            new PixelSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT),
            new Vector(96, 96),
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
        _vblankEvent.Set();
        Dispatcher.UIThread.Post(() =>
        {
            _vblankEvent.Reset();
            var now = DateTime.UtcNow;
            var elapsed = (now - _lastFrameTime).TotalMilliseconds;
            _lastFrameTime = now;

            double fps = 1000.0 / elapsed;
            double percentage = TARGET_FRAME_TIME_MS / elapsed * 100;
            FrameCounter++;
            FrameCounterString = $"Frame {FrameCounter} - {fps:F1}FPS - {percentage:F1}%";
            OnPropertyChanged(nameof(FrameCounterString));

            int w = ScreenDimensions.WIDTH;
            int h = ScreenDimensions.HEIGHT;
            var bmp = new WriteableBitmap(
                new PixelSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT),
                new Vector(96, 96),
                PixelFormat.Bgra8888,
                AlphaFormat.Premul);


            using var fb = bmp.Lock();
            int stride = fb.RowBytes;

            unsafe
            {
                byte* ptr = (byte*)fb.Address;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        byte fbPixel = e.FrameBuffer[y, x];
                        byte intensity = (byte)(255 - fbPixel * 85);
                        int idx = y * stride + x * 4;

                        ptr[idx + 0] = intensity;
                        ptr[idx + 1] = intensity;
                        ptr[idx + 2] = intensity;
                        ptr[idx + 3] = 255;
                    }
                }
            }

            FrameBitmap = bmp;
            OnPropertyChanged(nameof(FrameBitmap));
        });
    }

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}