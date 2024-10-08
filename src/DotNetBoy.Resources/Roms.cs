namespace DotNetBoy.Resources;

public static class Roms
{
    public static FileInfo[] RomFileInfos => new DirectoryInfo(@"..\..\..\..\DotNetBoy.Resources\DebugAssets").GetFiles("*.gb",SearchOption.AllDirectories);
}