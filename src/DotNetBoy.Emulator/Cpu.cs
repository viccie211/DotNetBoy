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
    IClockService clockService,
    IPpuService ppuService)
{
    private const byte INSTRUCTION_PREFIX = 0xCB;

    private InterruptRegister InterruptEnableRegister => mmuService.ReadByte(AddressConsts.INTERRUPT_ENABLE_REGISTER_ADDRESS);

    private byte interruptRequestRegister
    {
        get => mmuService.ReadByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS);
        set => mmuService.WriteByte(AddressConsts.INTERRUPT_REQUEST_REGISTER_ADDRESS, value);
    }

    private bool StatBlock = false;

    public void Loop()
    {
        while (!cpuRegistersService.Halted)
        {
            //Fetch
            var instruction = mmuService.ReadByte(cpuRegistersService.ProgramCounter);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = mmuService.ReadByte((ushort)(cpuRegistersService.ProgramCounter + 1));
                var decodedInstruction = instructionSetService.PrefixedInstructions[actualInstruction];
                decodedInstruction(cpuRegistersService);
            }
            else
            {
                var decodedInstruction = instructionSetService.NonPrefixedInstructions[instruction];
                decodedInstruction(cpuRegistersService);
            }

            Interrupts();

            if (cpuRegistersService.InterruptsJustEnabled)
            {
                cpuRegistersService.InterruptsJustEnabled = false;
            }
        }

        Console.WriteLine("Halted");
    }

    private void Interrupts()
    {
        if (!cpuRegistersService.InterruptMasterEnable || cpuRegistersService.InterruptsJustEnabled)
            return;

        InterruptRegister castRegister = interruptRequestRegister;

        if (InterruptEnableRegister.VBlank && castRegister.VBlank)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            castRegister.VBlank = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.VBLANK_INTERRUPT_VECTOR);
            ppuService.VBlankStartInvoke(this, EventArgs.Empty);
            return;
        }

        if (InterruptEnableRegister.LCD && castRegister.LCD && !StatBlock)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            castRegister.LCD = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.LCD_INTERRUPT_VECTOR);
            StatBlock = true;
            return;
        }

        StatBlock = false;

        if (InterruptEnableRegister.Timer && castRegister.Timer)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            castRegister.Timer = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.TIMER_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Serial && castRegister.Serial)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            castRegister.Serial = false;
            interruptRequestRegister = castRegister;
            CallInterruptVector(AddressConsts.SERIAL_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Joypad && castRegister.Joypad)
        {
            cpuRegistersService.InterruptMasterEnable = false;
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
        cpuRegistersService.StackPointer--;
        mmuService.WriteByte(cpuRegistersService.StackPointer, lower);
        cpuRegistersService.ProgramCounter = address;
        clockService.Clock(5, true);
    }
}