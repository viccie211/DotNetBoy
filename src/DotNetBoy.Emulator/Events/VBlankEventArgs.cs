using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.Emulator.Events;

public class VBlankEventArgs : EventArgs
{
    public int[,] FrameBuffer { get; set; } = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
}