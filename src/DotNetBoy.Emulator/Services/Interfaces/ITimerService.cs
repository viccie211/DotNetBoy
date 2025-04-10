namespace DotNetBoy.Emulator.Services.Interfaces;

public interface ITimerService
{
    ushort DivCounter { get; }
    byte Div { get; set; }

    byte Tima { get; set; }

    byte Tma { get; set; }

    byte Tac { get; set; }

    bool Tick();
}