using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.FrontEnd;

public class EmulatorScreenDrawable : IDrawable
{
    public int[,] FrameBuffer { get; set; } = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        for (var y = 0; y < ScreenDimensions.HEIGHT; y++)
        {
            for (var x = 0; x < ScreenDimensions.WIDTH; x++)
            {
                var gbColor = FrameBuffer[y, x];
                var color = 0.33f * gbColor;
                canvas.StrokeColor = Color.FromRgb(color, color, color);
                canvas.StrokeSize = 1;
                canvas.DrawRectangle(x, y, 1, 1);
            }
        }
    }
}