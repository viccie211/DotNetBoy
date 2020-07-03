using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace CoreBoy.Emulator
{
    public partial class CPU
    {
        #region Consts
        private const byte INSTRUCTION_PREFIX = 0xCB;
        private const ushort INTERRUPTS_FLAGS_ADDRESS = 0xFFFF;
        private const ushort INTERRUPTS_TRIGGERED_ADDRESS = 0xFF0F;
        private const ushort VERTICAL_BLANK_ISR = 0x0040;
        private const ushort LCD_STATUS_TRIGGGERS_ISR = 0x0048;
        private const ushort TIMER_OVERFLOW_ISR = 0x0050;
        private const ushort SERIAL_LINK_ISR = 0x0058;
        private const ushort JOYPAD_PRESS_ISR = 0x0060;
        #endregion

        #region Registers
        private byte _RegA;
        private FlagsRegister _RegF;
        private ushort _RegAF
        {
            get
            {
                return ByteUshortHelper.CombineBytes(_RegA, _RegF);
            }
            set
            {
                _RegA = ByteUshortHelper.UpperByteOfSixteenBits(value);
                _RegF = ByteUshortHelper.LowerByteOfSixteenBits(value);
            }
        }

        private byte _RegB;
        private byte _RegC;

        private ushort _RegBC
        {
            get
            {
                return ByteUshortHelper.CombineBytes(_RegB, _RegC);
            }
            set
            {
                _RegB = ByteUshortHelper.UpperByteOfSixteenBits(value);
                _RegC = ByteUshortHelper.LowerByteOfSixteenBits(value);
            }
        }

        private byte _RegD;
        private byte _RegE;

        private ushort _RegDE
        {
            get
            {
                return ByteUshortHelper.CombineBytes(_RegD, _RegE);
            }
            set
            {
                _RegD = ByteUshortHelper.UpperByteOfSixteenBits(value);
                _RegE = ByteUshortHelper.LowerByteOfSixteenBits(value);
            }
        }

        private byte _RegH;
        private byte _RegL;

        private ushort _RegHL
        {
            get
            {
                return ByteUshortHelper.CombineBytes(_RegH, _RegL);
            }
            set
            {
                _RegH = ByteUshortHelper.UpperByteOfSixteenBits(value);
                _RegL = ByteUshortHelper.LowerByteOfSixteenBits(value);
            }
        }

        private ushort _RegPC;
        private ushort _RegSP;
        private byte _RegM;
        private byte _RegT;
        #endregion

        public MMU _MMU = new MMU();
        public PPU _PPU;
        public bool _Halted = false;
        public bool InteruptsEnabled = true;
        public bool InterruptsToggled = false;

        #region Public Methods

        public CPU()
        {
            Reset();
            _PPU = new PPU(_MMU);
        }


        public void Reset()
        {
            _RegA = 0x11;
            _RegB = 0;
            _RegC = 0;
            _RegD = 0xFF;
            _RegE = 0X56;
            _RegF = 0x80;
            _RegH = 0;
            _RegL = 0x0D;
            _RegPC = 0x100;
            _RegSP = 0;
            _RegM = 0;
            _RegT = 0;

        }

        public void ExecuteInstruction(byte instruction, bool prefixed = false)
        {
            if (prefixed)
            {
                PrefixedInstructions(instruction);
            }
            else
            {
                NonPrefixedInstructions(instruction);
            }
        }

        public void Loop()
        {
            while (!_Halted)
            {
                byte instruction = _MMU.ReadByte(_RegPC);
                bool prefixed = false;

                if (instruction == INSTRUCTION_PREFIX)
                {
                    instruction = _MMU.ReadByte((ushort)(_RegPC + 1));
                    prefixed = true;
                }
                Console.WriteLine($"{_RegPC.ToString("x2")}:{instruction.ToString("x2")}");
                ExecuteInstruction(instruction, prefixed);

                _PPU.Step();
//                Interupts();
                if (_PPU.Line >= PPU._height + 10)
                {
                    _PPU.Line = 0;
                    _Halted = true;
                }
            }
        }
        #endregion

        #region Private Methods

        private void Interupts()
        {
            if ((InteruptsEnabled && !InterruptsToggled) || (!InteruptsEnabled && InterruptsToggled))
            {
                byte interruptEnableFlags = _MMU.ReadByte(INTERRUPTS_FLAGS_ADDRESS);
                byte interruptsTriggered = _MMU.ReadByte(INTERRUPTS_TRIGGERED_ADDRESS);
                ushort interruptToCall = 0;

                if (interruptToCall == 0 && ((interruptEnableFlags & 0b00000001) == 1 && (interruptsTriggered & 0b00000001) == 1))
                {
                    interruptToCall = VERTICAL_BLANK_ISR;
                }

                if (interruptToCall == 0 && ((interruptEnableFlags & 0b00000010) == 2 && (interruptsTriggered & 0b00000010) == 2))
                {
                    interruptToCall = LCD_STATUS_TRIGGGERS_ISR;
                }

                if (interruptToCall == 0 && ((interruptEnableFlags & 0b00000100) == 4 && (interruptsTriggered & 0b00000100) == 4))
                {
                    interruptToCall = TIMER_OVERFLOW_ISR;

                }

                if (interruptToCall == 0 && ((interruptEnableFlags & 0b00001000) == 8 && (interruptsTriggered & 0b00001000) == 8))
                {
                    interruptToCall = SERIAL_LINK_ISR;
                }

                if (interruptToCall == 0 && ((interruptEnableFlags & 0b00010000) == 16 && (interruptsTriggered & 0b00010000) == 16))
                {

                    interruptToCall = JOYPAD_PRESS_ISR;
                }

                CallInterrupt(interruptToCall);
            }

            if (InterruptsToggled)
            {
                InterruptsToggled = false;
            }
        }

        private void CallInterrupt(ushort address)
        {
            InteruptsEnabled = false;
            Push(_RegPC, this);
            _RegPC = address;
        }

        #endregion
    }
}
