using System;
using System.Collections.Generic;
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
        var buttonMappings = new Dictionary<EJoyPadButton, InputElement>
        {
            { EJoyPadButton.Up, Up },
            { EJoyPadButton.Down, Down },
            { EJoyPadButton.Left, Left },
            { EJoyPadButton.Right, Right },
            { EJoyPadButton.A, A },
            { EJoyPadButton.B, B },
            { EJoyPadButton.Select, Select },
            { EJoyPadButton.Start, Start }
        };

        foreach (var (button, element) in buttonMappings)
        {
            element.AddHandler(
                InputElement.PointerPressedEvent,
                (_, _) => ButtonPressed(button),
                RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                handledEventsToo: true
            );

            element.AddHandler(
                InputElement.PointerReleasedEvent,
                (_, _) => ButtonReleased(button),
                RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                handledEventsToo: true
            );
        }
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