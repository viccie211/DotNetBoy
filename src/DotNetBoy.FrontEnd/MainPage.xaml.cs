using DotNetBoy.Emulator.Consts;
using DotNetBoy.FrontEnd.ViewModels;

namespace DotNetBoy.FrontEnd;

public partial class MainPage : ContentPage
{
    int count = 0;

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
        Task.Run(() => viewModel.StartEmulation());
    }

    public void OnVBlankStart(object? sender, EventArgs e)
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
            drawable.FrameBuffer = viewModel.PpuService.FrameBuffer;
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
}