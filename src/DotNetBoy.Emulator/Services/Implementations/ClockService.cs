using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class ClockService(IMmuService mmuService) : IClockService
{
    private const ushort DividerRegisterAddress = 0xFF04;
    private const ushort TimerCounterRegisterAddress = 0xFF05;
    private const ushort TimerModuloAddress = 0xFF06;
    private const ushort TimerControlRegisterAddress = 0xFF07;


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

    public void Clock(int clockIncrement = 1, bool incrementIsMClock = false)
    {
        var increment = incrementIsMClock ? clockIncrement : clockIncrement * 4;

        for (int i = 0; i < increment; i++)
        {
            M++;
            _internalTimer++;
            Timers();
            OnMClock(this, new ClockEventArgs() { ClockValue = M });

            if (M % 4 == 3)
            {
                T++;
                OnTClock(this, new ClockEventArgs { ClockValue = T });
            }
        }
    }

    private void Timers()
    {
        if (_internalTimer == 4096)
            _internalTimer = 0;
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

        if (_internalTimer % 256 == 0)
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