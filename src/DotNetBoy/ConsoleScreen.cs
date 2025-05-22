using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Events;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy;

public class ConsoleScreen
{
    public ConsoleScreen(IEventService eventService)
    {
        eventService.VBlankStart += RenderScreen;
        Console.SetWindowSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT);
    }

    public void RenderScreen(object? sender, VBlankEventArgs e)
    {
        var frameBuffer = e.FrameBuffer;
        Console.SetWindowSize(ScreenDimensions.WIDTH, ScreenDimensions.HEIGHT);
        Console.SetCursorPosition(0, 0);
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