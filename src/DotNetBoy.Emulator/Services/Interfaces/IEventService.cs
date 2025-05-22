using DotNetBoy.Emulator.Events;

namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void ClockHandler(object? sender, ClockEventArgs e);

public delegate void RaiseInterruptHandler(object? sender, RaiseInterruptEventArgs e);

public delegate void VBlankStart(object? sender, VBlankEventArgs e);

public interface IEventService
{
    event ClockHandler TClock;
    void InvokeTClock(object? sender, ClockEventArgs e);
    event ClockHandler MClock;
    void InvokeMClock(object? sender, ClockEventArgs e);
    event RaiseInterruptHandler InterruptRaised;
    void InvokeInterruptRaised(object? sender, RaiseInterruptEventArgs e);

    event VBlankStart VBlankStart;
    void VBlankStartInvoke(object? sender, VBlankEventArgs e);
}