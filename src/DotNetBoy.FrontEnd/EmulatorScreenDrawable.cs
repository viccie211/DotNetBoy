using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.FrontEnd;

public class EmulatorScreenDrawable : IDrawable
{
    public int Scale { get; set; } = 1;
    public int[,] FrameBuffer { get; set; } = new int[ScreenDimensions.HEIGHT, ScreenDimensions.WIDTH];
    public bool Drawing = false;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Drawing = true;
        for (var y = 0; y < ScreenDimensions.HEIGHT; y++)
        {
            for (var x = 0; x < ScreenDimensions.WIDTH; x++)
            {
                var gbColor = FrameBuffer[y, x];
                var color = 0.33f * gbColor;
                canvas.StrokeColor = Color.FromRgb(color, color, color);
                canvas.StrokeSize = Scale;
                canvas.DrawRectangle(x * Scale, y * Scale, Scale, Scale);
            }
        }

        Drawing = false;
    }
}