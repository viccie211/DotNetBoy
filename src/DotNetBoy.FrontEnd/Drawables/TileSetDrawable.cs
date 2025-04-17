namespace DotNetBoy.FrontEnd.Drawables;

public class TileSetDrawable : IDrawable
{
    public int Scale { get; set; } = 1;
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        throw new NotImplementedException();
    }
}