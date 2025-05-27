using DotNetBoy.Emulator.Enums;
using DotNetBoy.Emulator.Events;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface IPpuService
{
    PpuModes Mode { get; }
    byte[,] FrameBuffer { get; }
    byte LyRegister { get; set; }
    int ScanLine { get; set; }
}