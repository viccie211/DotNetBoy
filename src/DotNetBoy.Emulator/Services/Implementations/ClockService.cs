using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ClockService(IMmuService mmuService, ITimerService timerService) : IClockService
{
    public byte M { get; set; } = 0;
    public byte T { get; set; } = 0;

    public void Clock(int clockIncrement = 1, bool incrementIsTClock = false)
    {
        var increment = incrementIsTClock ? clockIncrement : clockIncrement * 4;

        for (int i = 0; i < increment; i++)
        {
            T++;
            OnTClock(this, new ClockEventArgs() { ClockValue = T });

            if (T % 4 == 0)
            {
                M++;
                OnMClock(this, new ClockEventArgs { ClockValue = M });

                if (timerService.Tick())
                {
                    var interruptRequestRegister = (InterruptRegister)mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
                    interruptRequestRegister.Timer = true;
                    mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRequestRegister);
                }
            }
        }
    }


    public void Reset()
    {
        M = 0;
        T = 0;
    }

    public event ClockHandler TClock;

    private void OnTClock(object? sender, ClockEventArgs e)
    {
        TClock?.Invoke(sender, e);
    }

    public event ClockHandler MClock;

    private void OnMClock(object? sender, ClockEventArgs e)
    {
        MClock?.Invoke(sender, e);
    }
}