using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.Emulator.Events;

public class VBlankEventArgs : EventArgs
{
    public byte[,] FrameBuffer { get; set; } = new byte[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
}