using System.Diagnostics;
using DotNetBoy.Emulator.Consts;

namespace DotNetBoy.FrontEnd.Drawables;

public class EmulatorScreenDrawable : IDrawable
{
    public int Scale { get; set; } = 1;
    public int[,]? FrameBuffer { get; set; }
    public bool Drawing = false;

    private static readonly Color[] GbColors = new[]
    {
        Color.FromRgb(1f, 1f, 1f), // White
        Color.FromRgb(0.6f, 0.6f, 0.6f), // Light gray
        Color.FromRgb(0.3f, 0.3f, 0.3f), // Dark gray
        Color.FromRgb(0f, 0f, 0f), // Black
    };


    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        Drawing = true;

        if (FrameBuffer != null)
        {
            var fillColor = GbColors[0];
            canvas.FillColor = fillColor;
            var lastColor = 1;
            canvas.FillRectangle(0, 0, ScreenDimensions.WIDTH * Scale, ScreenDimensions.HEIGHT*Scale);
            for (var y = 0; y < ScreenDimensions.HEIGHT; y++)
            {
                for (var x = 0; x < ScreenDimensions.WIDTH; x++)
                {
                    var gbColor = FrameBuffer[y, x];
                    if (gbColor > 0)
                    {
                        if (lastColor != gbColor)
                        {
                            canvas.FillColor = GbColors[gbColor];
                            lastColor = gbColor;
                        }

                        canvas.FillRectangle(x * Scale, y * Scale, Scale, Scale);
                    }
                }
            }
        }

        Drawing = false;
    }
}