using System.Text.RegularExpressions;

namespace DotNetBoy;

public class Roms
{

    public static byte[] BgbTestRom = File.ReadAllBytes("..\\..\\..\\DebugAssets\\bgbtest.gb");

    public static byte[] InstructionsTestRom = File.ReadAllBytes("..\\..\\..\\DebugAssets\\01-special.gb");
    public static byte[] AllInstructionTest = File.ReadAllBytes("..\\..\\..\\DebugAssets\\cpu_instrs.gb");

    public static byte[] CustomTest = File.ReadAllBytes("..\\..\\..\\Assets\\CustomTest.gb");
    public static byte[] Tetris = File.ReadAllBytes("..\\..\\..\\DebugAssets\\Tetris.gb");
}