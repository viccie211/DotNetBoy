using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void VBlankStart(object? sender, EventArgs e);

public interface IPpuService
{
    PpuModes Mode { get; }
    event VBlankStart VBlankStart;
    void VBlankStartInvoke(object? sender, EventArgs e);
    int[,] FrameBuffer { get; }
    byte LyRegister { get; set; }
    int ScanLine { get; set; }
}