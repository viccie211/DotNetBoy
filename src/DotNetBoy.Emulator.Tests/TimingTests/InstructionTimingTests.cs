using DotNetBoy.Emulator.Extensions;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.BitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ResetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.RotateInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.SetBitInstructions;
using DotNetBoy.Emulator.InstructionSet.PrefixedInstructions.ShiftInstructions;
using DotNetBoy.Emulator.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.Emulator.Tests.TimingTests;

public class InstructionTimingTests
{
    private ICpuRegistersService _cpuRegistersService;
    private IMmuService _mmuService;
    private IByteUshortService _byteUshortService;
    private ITimerService _timerService;
    private IClockService _clockService;
    private Cpu _cpu;
    private int clockIncremented;

    private int[] instructionTimings =
    [
        1, 3, 2, 2, 1, 1, 2, 1, 5, 2, 2, 2, 1, 1, 2, 1,
        0, 3, 2, 2, 1, 1, 2, 1, 3, 2, 2, 2, 1, 1, 2, 1,
        2, 3, 2, 2, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 2, 1,
        2, 3, 2, 2, 3, 3, 3, 1, 2, 2, 2, 2, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        2, 2, 2, 2, 2, 2, 0, 2, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1,
        2, 3, 3, 4, 3, 4, 2, 4, 2, 4, 3, 0, 3, 6, 2, 4,
        2, 3, 3, 0, 3, 4, 2, 4, 2, 4, 3, 0, 3, 0, 2, 4,
        3, 3, 2, 0, 0, 4, 2, 4, 4, 1, 4, 0, 0, 0, 2, 4,
        3, 3, 2, 1, 0, 4, 2, 4, 3, 2, 4, 1, 0, 0, 2, 4
    ];


    [SetUp]
    public void SetUp()
    {
        var serviceCollection = new ServiceCollection();
        var clockServiceMock = new Mock<IClockService>();
        serviceCollection.AddScoped<IByteUshortService, ByteUshortService>();
        serviceCollection.AddScoped<ITimerService, TimerService>();
        serviceCollection.AddScoped<IMmuService, MmuService>();
        serviceCollection.AddScoped<IClockService>(provider =>
        {
            var underLyingClockService = new ClockService(provider.GetService<IMmuService>() ?? throw new InvalidOperationException(),
                provider.GetService<ITimerService>() ?? throw new InvalidOperationException());
            clockServiceMock.Setup(x => x.Clock(It.IsAny<int>(), It.IsAny<bool>())).Callback(new Action<int, bool>((arg1, arg2) =>
            {
                ClockPumped(arg1);
                underLyingClockService.Clock(arg1, arg2);
            }));
            return clockServiceMock.Object;
        });
        serviceCollection.AddScoped<ITileService, TileService>();
        serviceCollection.AddScoped<IPpuService, PpuService>();
        serviceCollection.AddScoped<ICpuRegistersService, CpuRegistersService>();
        serviceCollection.AddScoped<RotateInstructions>();
        serviceCollection.AddScoped<JumpInstructions>();
        serviceCollection.AddScoped<MiscellaneousInstructions>();
        serviceCollection.AddScoped<LoadInstructions>();
        serviceCollection.AddScoped<LoadBetweenRegistersInstructions>();
        serviceCollection.AddScoped<IncrementInstructions>();
        serviceCollection.AddScoped<DecrementInstructions>();
        serviceCollection.AddScoped<LogicInstructions>();
        serviceCollection.AddScoped<StoreInstructions>();
        serviceCollection.AddScoped<PushPopInstructions>();
        serviceCollection.AddScoped<ArithmeticInstructions>();
        serviceCollection.AddScoped<ShiftRightLogicalInstructions>();
        serviceCollection.AddScoped<ShiftRightArithmeticInstructions>();
        serviceCollection.AddScoped<ShiftLeftArithmeticInstructions>();
        serviceCollection.AddScoped<BitInBRegisterInstructions>();
        serviceCollection.AddScoped<BitInCRegisterInstructions>();
        serviceCollection.AddScoped<BitInDRegisterInstructions>();
        serviceCollection.AddScoped<BitInERegisterInstructions>();
        serviceCollection.AddScoped<BitInHRegisterInstructions>();
        serviceCollection.AddScoped<BitInLRegisterInstructions>();
        serviceCollection.AddScoped<BitAtAddressHLInstructions>();
        serviceCollection.AddScoped<BitInARegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInBRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInCRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInDRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInERegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInHRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInLRegisterInstructions>();
        serviceCollection.AddScoped<ResetBitAtAddressHLInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<ResetBitInARegisterInstructions>();
        serviceCollection.AddScoped<SetBitInBRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInCRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInDRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInERegisterInstructions>();
        serviceCollection.AddScoped<SetBitInHRegisterInstructions>();
        serviceCollection.AddScoped<SetBitInLRegisterInstructions>();
        serviceCollection.AddScoped<SetBitAtAddressHLInstructions>();
        serviceCollection.AddScoped<SetBitInARegisterInstructions>();
        serviceCollection.AddScoped<SetBitInARegisterInstructions>();
        serviceCollection.AddScoped<RotateRightInstructions>();
        serviceCollection.AddScoped<RotateLeftInstructions>();
        serviceCollection.AddScoped<RotateRightThroughCarryInstructions>();
        serviceCollection.AddScoped<RotateLeftThroughCarryInstructions>();
        serviceCollection.AddScoped<SwapInstructions>();

        serviceCollection.AddScoped<IInstructionSetService, InstructionSetService>();

        serviceCollection.AddScoped<Cpu>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        clockIncremented = 0;
        _cpu = scope.ServiceProvider.GetService<Cpu>() ?? throw new InvalidOperationException();
        _mmuService = scope.ServiceProvider.GetService<IMmuService>() ?? throw new InvalidOperationException();
        _cpuRegistersService = scope.ServiceProvider.GetService<ICpuRegistersService>() ?? throw new InvalidOperationException();
    }

    public void ClockPumped(int increment)
    {
        clockIncremented += increment;
    }

    [Test]
    public void NonPrefixeds()
    {
        int[] skips = [0x10,0x76, 0xCB, 0xD3, 0xDB, 0xDD, 0xE3, 0xE4, 0xEB, 0xEC, 0xEC, 0xED, 0xF4, 0xFC, 0xFD];
        for (int instr = 0; instr < instructionTimings.Length; instr++)
        {
            if (skips.Contains(instr))
                continue;
            _mmuService.WriteByteRaw(0xC000, (byte)instr);
            clockIncremented = 0;
            _cpuRegistersService.ProgramCounter = 0xC000;
            _cpu.Step();
            Assert.That(clockIncremented, Is.EqualTo(instructionTimings[instr]),()=>$"Instr:{instr:x2} Was {clockIncremented}, expected {instructionTimings[instr]}");
            
        }
    }
}