using DotNetBoy.Emulator.Consts;
using DotNetBoy.Emulator.InstructionSet;
using DotNetBoy.Emulator.Models;
using DotNetBoy.Emulator.Services.Implementations;
using DotNetBoy.Emulator.Services.Interfaces;

namespace DotNetBoy.Emulator;

public class Cpu(
    IMmuService mmuService,
    ICpuRegistersService cpuRegistersService,
    IInstructionSetService instructionSetService,
    IByteUshortService byteUshortService,
    IClockService clockService)
{
    public const byte INSTRUCTION_PREFIX = 0xCB;
    public byte Instruction { get; set; }
    public bool Prefixed { get; set; }

    private InterruptRegister InterruptEnableRegister => mmuService.ReadByte(AddressConsts.INTERRUPT_ENABLE_REGISTER_ADDRESS);

    private byte interruptRequestRegister
    {
        get => mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        set => mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, value);
    }

    private bool StatBlock = false;

    public void Loop()
    {
        while (true)
        {
            Step();
        }
    }

    public void Step()
    {
        Interrupts();
        if (cpuRegistersService.InterruptsJustEnabled)
        {
            cpuRegistersService.InterruptsJustEnabled = false;
        }

        if (cpuRegistersService.HaltBug)
        {
            cpuRegistersService.HaltBug = false;
            cpuRegistersService.ProgramCounter--;
        }

        if (cpuRegistersService.Halted)
        {
            clockService.Clock();
        }
        else
        {
            Instruction = mmuService.ReadByte(cpuRegistersService.ProgramCounter);
            clockService.Clock();

            if (Instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = mmuService.ReadByte((ushort)(cpuRegistersService.ProgramCounter + 1));

                Prefixed = true;
                Instruction = actualInstruction;
                instructionSetService.PrefixedInstruction(actualInstruction, cpuRegistersService);
            }
            else
            {
                Prefixed = false;
                instructionSetService.NonPrefixedInstruction(Instruction,cpuRegistersService);
            }
        }

        if (cpuRegistersService.Halted && (cpuRegistersService.InterruptMasterEnable || (InterruptEnableRegister & interruptRequestRegister & 0x1f) != 0))
        {
            cpuRegistersService.Halted = false;
            if (!cpuRegistersService.InterruptMasterEnable)
            {
                cpuRegistersService.ProgramCounter++;
            }
        }
    }

    private void Interrupts()
    {
        if ((InterruptEnableRegister & interruptRequestRegister & 0x1F) == 0)
            return;

        if (!cpuRegistersService.Halted)
        {
            clockService.Clock();
        }

        if (cpuRegistersService.InterruptsJustEnabled)
            return;

        cpuRegistersService.Halted = false;

        if (!cpuRegistersService.InterruptMasterEnable)
            return;

        clockService.Clock();
        InterruptRegister castRegister = interruptRequestRegister;
        cpuRegistersService.InterruptMasterEnable = false;

        if (InterruptEnableRegister.VBlank && castRegister.VBlank)
        {
            castRegister.VBlank = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.VBLANK_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.LCD && castRegister.LCD && !StatBlock)
        {
            castRegister.LCD = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.LCD_INTERRUPT_VECTOR);
            StatBlock = true;
            return;
        }

        StatBlock = false;

        if (InterruptEnableRegister.Timer && castRegister.Timer)
        {
            castRegister.Timer = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.TIMER_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Serial && castRegister.Serial)
        {
            castRegister.Serial = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.SERIAL_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Joypad && castRegister.Joypad)
        {
            castRegister.Joypad = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.JOYPAD_INTERRUPT_VECTOR);
            return;
        }
    }

    private void CallInterruptVector(ushort address)
    {
        var toStore = cpuRegistersService.ProgramCounter;
        var lower = byteUshortService.LowerByteOfSixteenBits(toStore);
        var upper = byteUshortService.UpperByteOfSixteenBits(toStore);
        cpuRegistersService.StackPointer--;
        mmuService.WriteByte(cpuRegistersService.StackPointer, upper);
        clockService.Clock();
        cpuRegistersService.StackPointer--;
        mmuService.WriteByte(cpuRegistersService.StackPointer, lower);
        clockService.Clock();
        cpuRegistersService.ProgramCounter = address;
        clockService.Clock();
    }
}