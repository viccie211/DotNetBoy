using DotNetBoy.Emulator.Events;
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

    public event RaiseInterruptHandler? InterruptRaised;

    public void InvokeInterruptRaised(object? sender, RaiseInterruptEventArgs e)
    {
        InterruptRaised?.Invoke(sender, e);
    }

    public event VBlankStart? VBlankStart;

    public void VBlankStartInvoke(object? sender, VBlankEventArgs e)
    {
        VBlankStart?.Invoke(sender, e);
    }
}