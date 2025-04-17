namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void ClockHandler(object? sender, ClockEventArgs e);

public interface IEventService
{
    event ClockHandler TClock;

    event ClockHandler MClock;
    void InvokeTClock(object? sender, ClockEventArgs e);
    void InvokeMClock(object? sender, ClockEventArgs e);
}