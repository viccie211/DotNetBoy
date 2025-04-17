namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IClockService
{
    public byte M { get; set; }
    public byte T { get; set; }
    void Clock(int clockIncrement = 1, bool incrementIsTClock = false);
    void Reset();
}