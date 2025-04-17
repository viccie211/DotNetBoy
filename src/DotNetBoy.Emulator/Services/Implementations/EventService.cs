using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class EventService : IEventService
{
    public event ClockHandler TClock;

    public void InvokeTClock(object? sender, ClockEventArgs e)
    {
        TClock?.Invoke(sender, e);
    }

    public event ClockHandler MClock;

    public void InvokeMClock(object? sender, ClockEventArgs e)
    {
        MClock?.Invoke(sender, e);
    }
}