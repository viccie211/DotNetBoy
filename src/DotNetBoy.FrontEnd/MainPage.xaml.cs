using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Models;
using DotNetBoy.FrontEnd.Drawables;
using DotNetBoy.FrontEnd.ViewModels;

namespace DotNetBoy.FrontEnd;

public partial class MainPage : ContentPage
{
    int count = 0;
    private bool paused;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.PpuService.VBlankStart += OnVBlankStart;
        Dispatcher.DispatchAsync(() =>
        {
            EmulatorScreen.WidthRequest = ScreenDimensions.WIDTH * viewModel.Scale;
            EmulatorScreen.HeightRequest = ScreenDimensions.HEIGHT * viewModel.Scale;
            EmulatorScreen.Invalidate();
        });
    }

    private void StartButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MainPageViewModel;
        Task.Run(() =>
        {
            var drawable = EmulatorScreen.Drawable as EmulatorScreenDrawable;
            var viewModel = BindingContext as MainPageViewModel;
            if (drawable == null || viewModel == null)
                throw new Exception();
            while (true)
            {
                if (!drawable.Drawing && !paused)
                {
                    viewModel.Cpu.Step();
                }
            }
        });
    }

    public void OnVBlankStart(object? sender, VBlankEventArgs e)
    {
        count++;

        var viewModel = BindingContext as MainPageViewModel;
        var frameBuffer = viewModel?.PpuService.FrameBuffer;
        if (frameBuffer == null)
            return;
        Dispatcher.DispatchAsync(() =>
        {
            CounterBtn.Text = $"VBlanked {count} times";
            var drawable = EmulatorScreen.Drawable as EmulatorScreenDrawable;
            drawable.FrameBuffer = e.FrameBuffer;
            drawable.Scale = viewModel.Scale;
            EmulatorScreen.WidthRequest = ScreenDimensions.WIDTH * viewModel.Scale;
            EmulatorScreen.HeightRequest = ScreenDimensions.HEIGHT * viewModel.Scale;
            EmulatorScreen.Invalidate();
        });
    }

    public void StepButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MainPageViewModel;
        PC.Text = viewModel.CpuRegistersService.ToString();
        viewModel.Cpu.Step();
    }

    public void PauseButtonClicked(object sender, EventArgs e)
    {
        paused = !paused;
    }
}