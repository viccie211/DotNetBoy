using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ClockService : IClockService
{
    public byte M { get; set; } = 0;
    public byte T { get; set; } = 0;

    public void Clock(int clockIncrement = 1, bool incrementIsMClock = false)
    {
        var increment = incrementIsMClock ? clockIncrement : clockIncrement * 4;

        for (int i = 0; i < increment; i++)
        {
            M++;
            OnMClock(this, new ClockEventArgs() { ClockValue = M });

            if (M % 4 == 3)
            {
                T++;
                OnTClock(this, new ClockEventArgs { ClockValue = T });
            }
        }
    }

    public void Reset()
    {
        M = 0;
        T = 0;
    }

    public event ClockHandler TClock;

    private void OnTClock(object? sender, ClockEventArgs e)
    {
        TClock?.Invoke(sender, e);
    }

    public event ClockHandler MClock;

    private void OnMClock(object? sender, ClockEventArgs e)
    {
        MClock?.Invoke(sender, e);
    }
}