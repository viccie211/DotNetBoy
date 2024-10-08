namespace DotNetBoy.Resources;

public static class Roms
{
    public static FileInfo[] RomFileInfos => new DirectoryInfo(@"D:\Work\Personal\DotNetBoy\src\DotNetBoy.Resources\DebugAssets").GetFiles("*.gb",SearchOption.AllDirectories);

    public static byte[] GetRom(string romName)
    {
        return File.ReadAllBytes(RomFileInfos.First(f => f.Name == romName).FullName);
    }
}