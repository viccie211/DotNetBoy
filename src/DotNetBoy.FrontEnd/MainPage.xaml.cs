using DotNetBoy.Emulator;
using DotNetBoy.Emulator.Services.Interfaces;
using DotNetBoy.FrontEnd.ViewModels;

namespace DotNetBoy.FrontEnd;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage(MainPageViewModel viewModel)
    {

        InitializeComponent();
        BindingContext = viewModel;
        viewModel._ppuService.VBlankStart += OnVBlankStart;
    }

    private void StartButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as MainPageViewModel;
        Task.Run(() => viewModel.StartEmulation());
    }

    public void OnVBlankStart(object? sender, EventArgs e)
    {
        count++;
        Dispatcher.DispatchAsync(() =>
        {
            CounterBtn.Text = $"VBlanked {count} times";
        });
        

    }
}