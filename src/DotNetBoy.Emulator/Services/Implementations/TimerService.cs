using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator.Services.Implementations;

public class TimerService : ITimerService
{
    // TimerService implementation is mostly based on Timers.cs from SpecBoy. I couldn't get my own implementation to work properly, so I stole it :/.
    private static readonly int[] triggerBits = [9, 3, 5, 7];

    private int divBit;
    public ushort divCounter;
    public ushort DivCounter => divCounter;
    private bool lastResult;
    private bool reloadTima;
    private bool reloadingTima;

    // Registers
    private byte tima;
    private byte tma;
    private byte tac;

    public byte Div
    {
        get => (byte)(divCounter >> 8);

        set
        {
            // Writing to Div always sets it to 0
            divCounter = 0;

            // Can't have falling edge if last result was 0
            if (!lastResult)
            {
                return;
            }

            DetectFallingEdge();
        }
    }

    public byte Tima
    {
        get => tima;
        set
        {
            // Block write if TIMA is being reloaded
            if (reloadingTima)
            {
                return;
            }

            // Writing to TIMA can cancel IRQ request, do that here
            tima = value;
            reloadTima = false;
        }
    }

    public byte Tma
    {
        get => tma;
        set
        {
            tma = value;

            // TIMA also updated if it's being reloaded when TMA written to
            if (reloadingTima)
            {
                tima = tma;
            }
        }
    }

    public byte Tac
    {
        get => tac;
        set
        {
            tac = value;

            divBit = triggerBits[tac & 3];

            // Can't have falling edge if last result was 0
            if (!lastResult)
            {
                return;
            }

            DetectFallingEdge();
        }
    }

    public bool Tick()
    {
        var raiseInterrupt = false;
        reloadingTima = false;

        if (reloadTima)
        {
            reloadTima = false;
            reloadingTima = true;
            tima = tma;

            raiseInterrupt = true;
        }

        divCounter += 4;
        DetectFallingEdge();
        return raiseInterrupt;
    }

    private void DetectFallingEdge()
    {
        bool result = IsBitSet(2, tac) && IsBitSet(divBit, divCounter);

        // Detect falling edge
        if (lastResult && !result)
        {
            tima++;

            if (tima == 0)
            {
                reloadTima = true;
            }
        }

        lastResult = result;
    }

    private bool IsBitSet(int bitNumber, uint toTest)
    {
        var mask = 1 << bitNumber;
        return (mask & toTest) == mask;
    }
}