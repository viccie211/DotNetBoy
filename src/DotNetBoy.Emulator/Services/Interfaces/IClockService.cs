public interface IClockService
{
    public byte M { get; set; }
    public byte T { get; set; }
    void Clock(int clockIncrement = 1);
    void Reset();
    event EventHandler<ClockEventArgs> TClock;
    event EventHandler<ClockEventArgs> MClock;
}