using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia;
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
    public string FrameCounterString { get; set; } = "Frame 0";
    public int FrameCounter { get; set; }

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
        Dispatcher.UIThread.Post(() =>
        {
            int w = ScreenDimensions.WIDTH;
            int h = ScreenDimensions.HEIGHT;
            var bmp = new WriteableBitmap(
                new PixelSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT),
                new Vector(96, 96),
                PixelFormat.Bgra8888,
                AlphaFormat.Premul);


            using var fb = bmp.Lock();
            int stride = fb.RowBytes;
            byte[] pixelData = new byte[stride * h];
            Task[] tasks = new Task[h * w];

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    var taskY = y;
                    var taskX = x;
                    tasks[y*w+x] =Task.Run(()=>SetPixel(taskY, taskX, e.FrameBuffer, stride, pixelData));
                }
            }

            Task.WaitAll(tasks);
            Marshal.Copy(pixelData, 0, fb.Address, pixelData.Length);
            FrameCounter++;
            FrameCounterString = $"Frame {FrameCounter}";
            FrameBitmap = bmp;
            OnPropertyChanged(nameof(FrameBitmap));
            OnPropertyChanged(nameof(FrameCounterString));
        });
    }

    private void SetPixel(int y, int x, int[,] frameBuffer, int stride, byte[] pixelData)
    {
        var gray = frameBuffer[y, x];
        byte intensity = (byte)(255 - gray * 85);
        int idx = y * stride + x * 4;

        pixelData[idx + 0] = intensity;
        pixelData[idx + 1] = intensity;
        pixelData[idx + 2] = intensity;
        pixelData[idx + 3] = 255;
    }

    public bool Drawing { get; set; }

    protected void OnPropertyChanged(string name)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}