namespace DotNetBoy.Emulator.Services.Interfaces;

public delegate void VBlankStart(object? sender, EventArgs e);

public interface IPpuService
{
    event VBlankStart VBlankStart;
}