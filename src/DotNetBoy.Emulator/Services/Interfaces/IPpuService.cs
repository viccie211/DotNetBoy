using DotNetBoy.Emulator.Enums;

namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void VBlankStart(object? sender, EventArgs e);

public interface IPpuService
{
    PpuModes Mode { get; }
    event VBlankStart VBlankStart;
    int[,] FrameBuffer { get; }
}