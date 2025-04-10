using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy;

public class ConsoleScreen
{
    private readonly IPpuService _ppuService;

    public ConsoleScreen(IPpuService ppuService)
    {
        _ppuService = ppuService;
        _ppuService.VBlankStart += RenderScreen;
        Console.SetWindowSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT);
    }

    public void RenderScreen(object? sender, EventArgs e)
    {
        var frameBuffer = _ppuService.FrameBuffer;
        Console.SetWindowSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT);
        Console.SetCursorPosition(0,0);
        for (int y = 0; y < ScreenDimensions.HEIGHT; y++)
        {
            for (int x = 0; x < ScreenDimensions.WIDTH; x++)
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