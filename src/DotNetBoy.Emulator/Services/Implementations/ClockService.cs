using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ClockService(IMmuService mmuService, ITimerService timerService, IEventService eventService) : IClockService
{
    public byte M { get; set; } = 0;
    public byte T { get; set; } = 0;

    public void Clock(int clockIncrement = 1, bool incrementIsTClock = false)
    {
        var increment = incrementIsTClock ? clockIncrement : clockIncrement * 4;

        for (int i = 0; i < increment; i++)
        {
            T++;
            eventService.InvokeTClock(this, new ClockEventArgs() { ClockValue = T });

            if (T % 4 == 0)
            {
                M++;
                eventService.InvokeMClock(this, new ClockEventArgs { ClockValue = M });

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
}