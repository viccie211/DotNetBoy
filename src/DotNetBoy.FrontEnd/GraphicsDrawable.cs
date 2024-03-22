namespace DotNetBoy.FrontEnd;

public class GraphicsDrawable : IDrawable
{
    private int frame = 0;

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Red;
        canvas.StrokeSize = 6;
        canvas.DrawLine(10 + frame, 10, 90, 100);
    }
}