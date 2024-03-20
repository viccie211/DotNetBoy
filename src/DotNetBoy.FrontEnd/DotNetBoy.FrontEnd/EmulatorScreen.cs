using Eto.Drawing;

namespace DotNetBoy.FrontEnd;

public class EmulatorScreen
{
    public Bitmap Bitmap { get; internal set; }
    public int Scale { get; set; } = 1;

    public EmulatorScreen()
    {
        Bitmap = new Bitmap(160 * Scale, 144 * Scale, PixelFormat.Format24bppRgb);
        
    }
}