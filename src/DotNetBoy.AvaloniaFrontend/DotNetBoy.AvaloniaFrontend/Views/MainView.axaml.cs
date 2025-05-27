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
        List<(EJoyPadButton, InputElement, Key)> buttonMappings =
        [
            new(EJoyPadButton.Up, Up, Key.Up),
            new(EJoyPadButton.Down, Down, Key.Down),
            new(EJoyPadButton.Left, Left, Key.Left),
            new(EJoyPadButton.Right, Right, Key.Right),
            new(EJoyPadButton.A, A, Key.S),
            new(EJoyPadButton.B, B, Key.D),
            new(EJoyPadButton.Select, Select, Key.RightShift), // Or Key.Back if you prefer
            new(EJoyPadButton.Start, Start, Key.Enter)
        ];
        foreach (var mapping in buttonMappings)
        {
            mapping.Item2.AddHandler(
                InputElement.PointerPressedEvent,
                (_, _) => ButtonPressed(mapping.Item1),
                RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                handledEventsToo: true
            );

            mapping.Item2.AddHandler(
                InputElement.PointerReleasedEvent,
                (_, _) => ButtonReleased(mapping.Item1),
                RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                handledEventsToo: true
            );
            Loaded += (_, _) =>
            {
                var topLevel = TopLevel.GetTopLevel(this)!;
                topLevel.AddHandler(KeyDownEvent, (sender, args) =>
                    {
                        if (args.Key == mapping.Item3)
                            ButtonPressed(mapping.Item1);
                    },
                    RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                    handledEventsToo:
                    true);
                topLevel.AddHandler(KeyUpEvent, (sender, args) =>
                    {
                        if (args.Key == mapping.Item3)
                            ButtonReleased(mapping.Item1);
                    },
                    RoutingStrategies.Tunnel | RoutingStrategies.Bubble,
                    handledEventsToo: true
                );
            };
        }
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