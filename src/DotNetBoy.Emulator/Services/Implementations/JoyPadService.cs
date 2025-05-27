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

    public bool SelectButtons { get; set; } = false;
    public bool SelectDPad { get; set; } = false;

    public JoyPadRegister Register
    {
        get
        {
            var result = new JoyPadRegister()
            {
                SelectButtons = SelectButtons,
                SelectDPad = SelectDPad,
            };

            // If both are selected the GameBoy ANDs the status of the buttons
            if (SelectButtons && SelectDPad)
            {
                result.StartOrDown = Status[EJoyPadButton.Start] && Status[EJoyPadButton.Down];
                result.SelectOrUp = Status[EJoyPadButton.Select] && Status[EJoyPadButton.Up];
                result.AOrRight = Status[EJoyPadButton.A] && Status[EJoyPadButton.Right];
                result.BOrLeft = Status[EJoyPadButton.B] && Status[EJoyPadButton.Left];
                return result;
            }

            if (SelectButtons)
            {
                result.StartOrDown = Status[EJoyPadButton.Start];
                result.SelectOrUp = Status[EJoyPadButton.Select];
                result.AOrRight = Status[EJoyPadButton.A];
                result.BOrLeft = Status[EJoyPadButton.B];
                return result;
            }

            if (SelectDPad)
            {
                result.StartOrDown = Status[EJoyPadButton.Down];
                result.SelectOrUp = Status[EJoyPadButton.Up];
                result.AOrRight = Status[EJoyPadButton.Right];
                result.BOrLeft = Status[EJoyPadButton.Left];
                return result;
            }

            return result;
        }
    }

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