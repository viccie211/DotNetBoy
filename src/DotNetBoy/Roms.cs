using System.Text.RegularExpressions;

namespace DotNetBoy;

public class Roms
{

    public static byte[] BgbTestRom = File.ReadAllBytes("..\\..\\..\\DebugAssets\\bgbw64\\bgbtest.gb");

    public static byte[] LoadInstructionsTestRom = File.ReadAllBytes("..\\..\\..\\DebugAssets\\bgbw64\\06-ld r,r.gb");

    public static byte[] CustomTest = File.ReadAllBytes("..\\..\\..\\Assets\\CustomTest.gb");
    public static byte[] Tetris = File.ReadAllBytes("..\\..\\..\\DebugAssets\\Tetris.gb");
}