using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy;

public class ConsoleScreen
{
    private readonly IPpuService _ppuService;

    public ConsoleScreen(IPpuService ppuService)
    {
        _ppuService = ppuService;
        _ppuService.VBlankStart += RenderScreen;
        Console.SetWindowSize(160,144);
    }

    public void RenderScreen(object? sender, EventArgs e)
    {
        var frameBuffer = _ppuService.FrameBuffer;
        Console.SetCursorPosition(0,0);
        for (int y = 0; y < 144; y++)
        {
            for (int x = 0; x < 160; x++)
            {
                char toWrite = ' ';
                switch (frameBuffer[y, x])
                {
                    case 0:
                        toWrite = ' ';
                        break;
                    case 1:
                        toWrite = '░';
                        break;
                    case 2:
                        toWrite = '▒';
                        break;
                    case 3:
                        toWrite = '▓';
                        break;
                }

                Console.Write(toWrite);
            }

            Console.Write('\n');
        }
    }
}