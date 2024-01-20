public delegate void TClockNotify();
public delegate void MClockNotify();
public class ClockService : IClockService
{

    public byte M { get; set; } = 0;
    public byte T { get; set; } = 0;

    public void Clock(int clockIncrement = 1)
    {
        for (int i = 0; i < clockIncrement * 4; i++)
        {
            if (i % 4 == 0)
            {
                T = (byte)(T + i);
                OnTClock(this, new ClockEventArgs { ClockValue = T });
            }
            M = (byte)(M + i);
            OnMClock(this, new ClockEventArgs() { ClockValue = M });
        }
    }

    public void Reset()
    {
        M = 0;
        T = 0;
    }

    public event EventHandler<ClockEventArgs> TClock;

    protected virtual void OnTClock(object? sender, ClockEventArgs e)
    {
        TClock?.Invoke(sender, e);
    }
    public event EventHandler<ClockEventArgs> MClock;

    protected virtual void OnMClock(object? sender, ClockEventArgs e)
    {
        MClock?.Invoke(sender, e);
    }
}