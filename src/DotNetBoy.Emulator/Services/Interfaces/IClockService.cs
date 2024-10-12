namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void ClockHandler(object? sender, ClockEventArgs e);

public interface IClockService
{
    public byte M { get; set; }
    public byte T { get; set; }
    void Clock(int clockIncrement = 1, bool incrementIsTClock = false);
    void Reset();
    event ClockHandler TClock;
    event ClockHandler MClock;
}