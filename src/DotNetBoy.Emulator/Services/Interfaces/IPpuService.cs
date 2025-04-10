using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Events;

namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void VBlankStart(object? sender, VBlankEventArgs e);

public interface IPpuService
{
    PpuModes Mode { get; }
    event VBlankStart VBlankStart;
    void VBlankStartInvoke(object? sender, VBlankEventArgs e);
    int[,] FrameBuffer { get; }
    byte LyRegister { get; set; }
    int ScanLine { get; set; }
}