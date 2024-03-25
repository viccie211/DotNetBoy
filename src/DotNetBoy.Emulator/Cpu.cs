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
    public const ushort INTERRUPT_REQUEST_REGISTER_ADDRESS = 0xFF0F;
    private const byte INSTRUCTION_PREFIX = 0xCB;
    private const ushort INTERRUPT_ENABLE_REGISTER_ADDRESS = 0xFFFF;
    private const ushort VBLANK_INTERRUPT_VECTOR = 0x0040;
    private const ushort LCD_INTERRUPT_VECTOR = 0x0048;
    private const ushort TIMER_INTERRUPT_VECTOR = 0x0050;
    private const ushort SERIAL_INTERRUPT_VECTOR = 0x0058;
    private const ushort JOYPAD_INTERRUPT_VECTOR = 0x0060;
    private InterruptRegister InterruptEnableRegister => mmuService.ReadByte(INTERRUPT_ENABLE_REGISTER_ADDRESS);

    private InterruptRegister interruptRequestRegister
    {
        get => mmuService.ReadByte(INTERRUPT_REQUEST_REGISTER_ADDRESS);
        set => mmuService.WriteByte(INTERRUPT_REQUEST_REGISTER_ADDRESS, value);
    }

    public void Loop()
    {
        while (!cpuRegistersService.Halted)
        {
            //Fetch
            var instruction = mmuService.ReadByte(cpuRegistersService.ProgramCounter);

            if (instruction == INSTRUCTION_PREFIX)
            {
                var actualInstruction = mmuService.ReadByte((ushort)(cpuRegistersService.ProgramCounter + 1));
                var decodedInstruction = instructionSetService.PrefixedInstructions[actualInstruction] ??
                                         throw new NotImplementedException(
                                             $"\nPrefixed instruction {actualInstruction:X2} not implemented");
                decodedInstruction(cpuRegistersService);
            }
            else
            {
                var decodedInstruction = instructionSetService.NonPrefixedInstructions[instruction] ??
                                         throw new NotImplementedException(
                                             $"\nNonPrefixed instruction {instruction:X2} not implemented");
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

        InterruptRegister copiedRegister = (byte)interruptRequestRegister;

        if (InterruptEnableRegister.VBlank && interruptRequestRegister.VBlank)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            copiedRegister.VBlank = false;
            interruptRequestRegister = copiedRegister;
            CallInterruptVector(VBLANK_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.LCD && interruptRequestRegister.LCD)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            copiedRegister.LCD = false;
            interruptRequestRegister = copiedRegister;
            CallInterruptVector(LCD_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Timer && interruptRequestRegister.Timer)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            copiedRegister.Timer = false;
            interruptRequestRegister = copiedRegister;
            CallInterruptVector(TIMER_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Serial && interruptRequestRegister.Serial)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            copiedRegister.Serial = false;
            interruptRequestRegister = copiedRegister;
            CallInterruptVector(SERIAL_INTERRUPT_VECTOR);
            return;
        }

        if (InterruptEnableRegister.Joypad && interruptRequestRegister.Joypad)
        {
            cpuRegistersService.InterruptMasterEnable = false;
            copiedRegister.Joypad = false;
            interruptRequestRegister = copiedRegister;
            CallInterruptVector(JOYPAD_INTERRUPT_VECTOR);
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