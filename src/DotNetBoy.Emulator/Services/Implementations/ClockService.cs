using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ClockService(IMmuService mmuService) : IClockService
{
    private const ushort DividerRegisterAddress = AddressConsts.DIV_REGISTER;
    private const ushort TimerCounterRegisterAddress = AddressConsts.TIMA_REGISTER;
    private const ushort TimerModuloAddress = AddressConsts.TMA_REGISTER;
    private const ushort TimerControlRegisterAddress = AddressConsts.TAC_REGISTER;
    public byte M { get; set; } = 0;
    public byte T { get; set; } = 0;

    private uint _internalTimer = 0;

    private TimerControlRegister TimerControlRegister => mmuService.ReadByte(TimerControlRegisterAddress);

    private byte DividerRegister
    {
        get => mmuService.ReadByte(DividerRegisterAddress);
        set => mmuService.WriteByteRaw(DividerRegisterAddress, value);
    }

    private byte TimerCounterRegister
    {
        get => mmuService.ReadByte(TimerCounterRegisterAddress);
        set => mmuService.WriteByteRaw(TimerCounterRegisterAddress, value);
    }

    private byte TimerModuloRegister => mmuService.ReadByte(TimerModuloAddress);

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
                Timers();
                OnMClock(this, new ClockEventArgs { ClockValue = M });
            }
        }
    }

    private void Timers()
    {
        _internalTimer++;

        if (TimerControlRegister.TimerEnable && _internalTimer % TimerControlRegister.TimerInputDivisionFactor == 0)
        {
            if (TimerCounterRegister == 0xFF)
            {
                TimerCounterRegister = TimerModuloRegister;
                InterruptRegister interruptRegister = mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
                interruptRegister.Timer = true;
                mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, interruptRegister);
            }
            else
            {
                TimerCounterRegister++;
            }
        }

        if (_internalTimer % 16 == 0)
        {
            DividerRegister++;
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