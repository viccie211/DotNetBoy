using DotNetBoy.Emulator.Models;

namespace DotNetBoy.Emulator.Services.Interfaces;

public interface ICpuRegistersService
{
    bool InterruptMasterEnable { get; set; }
    byte A { get; set; }
    byte B { get; set; }
    byte C { get; set; }
    byte D { get; set; }
    byte E { get; set; }
    FlagsRegister F { get; set; }
    byte H { get; set; }
    byte L { get; set; }
    ushort AF { get; set; }
    ushort BC { get; set; }
    ushort DE { get; set; }
    ushort HL { get; set; }
    ushort ProgramCounter { get; set; }
    ushort StackPointer { get; set; }
    bool Halted { get; set; }

    void Reset();

    string ToString();
}