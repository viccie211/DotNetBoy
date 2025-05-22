using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class JoyPadService(IEventService eventService) : IJoyPadService
{
    public Dictionary<EJoyPadButton, bool> Status { get; } = new()
    {
        { EJoyPadButton.Down, false },
        { EJoyPadButton.Up, false },
        { EJoyPadButton.Left, false },
        { EJoyPadButton.Right, false },
        { EJoyPadButton.Start, false },
        { EJoyPadButton.Select, false },
        { EJoyPadButton.B, false },
        { EJoyPadButton.A, false }
    };

    public bool SelectButtons { get; set; } = true;
    public bool SelectDPad { get; set; } = false;

    public JoyPadRegister Register => new()
    {
        SelectButtons = SelectButtons,
        SelectDPad = SelectDPad,
        StartOrDown = (SelectButtons && Status[EJoyPadButton.Start]) || (SelectDPad && Status[EJoyPadButton.Down]),
        SelectOrUp = (SelectButtons && Status[EJoyPadButton.Select]) || (SelectDPad && Status[EJoyPadButton.Up]),
        BOrLeft = (SelectButtons && Status[EJoyPadButton.B]) || (SelectDPad && Status[EJoyPadButton.Left]),
        AOrRight = (SelectButtons && Status[EJoyPadButton.A]) || (SelectDPad && Status[EJoyPadButton.Right])
    };

    public void PressButtons(params EJoyPadButton[] buttons)
    {
        foreach (var button in buttons)
        {
            Status[button] = true;
        }

        if (buttons.Length != 0)
        {
            eventService.InvokeInterruptRaised(this, new() { InterruptRegister = new InterruptRegister() { Joypad = true } });
        }
    }

    public void ReleaseButtons(params EJoyPadButton[] buttons)
    {
        foreach (var button in buttons)
        {
            Status[button] = false;
        }
    }
}