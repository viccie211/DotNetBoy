using DotNetBoy.Emulator.Models;

namespace DotNetBoy.Emulator.Events;

public class RaiseInterruptEventArgs
{
    public InterruptRegister InterruptRegister { get; set; }
}