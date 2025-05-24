using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DotNetBoy.AvaloniaFrontend.ViewModels;
using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.AvaloniaFrontend.Views;

public partial class MainView : UserControl
{
    private readonly MainViewModel _dataContext;

    public MainView()
    {
        InitializeComponent();
        _dataContext = new MainViewModel();
        DataContext = _dataContext;
        Start.AddHandler(InputElement.PointerPressedEvent, ButtonStartPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble, handledEventsToo: true);
        Start.AddHandler(InputElement.PointerReleasedEvent, ButtonStartReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble, handledEventsToo: true);
        A.AddHandler(InputElement.PointerPressedEvent, ButtonAPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble, handledEventsToo: true);
        A.AddHandler(InputElement.PointerReleasedEvent, ButtonAReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble, handledEventsToo: true);
    }

    private void ButtonStartPressed(object? sender, RoutedEventArgs e)
    {
        ButtonPressed(EJoyPadButton.Start);
    }

    private void ButtonStartReleased(object? sender, RoutedEventArgs e)
    {
        ButtonReleased(EJoyPadButton.Start);
    }

    private void ButtonAPressed(object? sender, RoutedEventArgs e)
    {
        ButtonPressed(EJoyPadButton.A);
    }

    private void ButtonAReleased(object? sender, RoutedEventArgs e)
    {
        ButtonReleased(EJoyPadButton.A);
    }

    private void ButtonPressed(EJoyPadButton button)
    {
        _dataContext.JoyPadService.PressButtons(button);
    }

    private void ButtonReleased(EJoyPadButton button)
    {
        _dataContext.JoyPadService.ReleaseButtons(button);
    }
}