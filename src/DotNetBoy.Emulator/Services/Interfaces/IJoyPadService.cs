using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Models;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IJoyPadService
{
    Dictionary<EJoyPadButton, bool> Status { get; }
    void PressButtons(params EJoyPadButton[] buttons);
    void ReleaseButtons(params EJoyPadButton[] buttons);
    bool SelectButtons { get; set; }
    bool SelectDPad { get; set; }
    JoyPadRegister Register { get; }
}