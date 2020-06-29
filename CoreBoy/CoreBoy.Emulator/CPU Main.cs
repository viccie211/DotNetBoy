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
        private bool _Halted = false;

        #region Public Methods

        public CPU()
        {
            Reset();
        }


        public void Reset()
        {
            _RegA = 0;
            _RegB = 0;
            _RegC = 0;
            _RegD = 0;
            _RegE = 0;
            _RegF = 0;
            _RegH = 0;
            _RegL = 0;
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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
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
                if (stopwatch.ElapsedMilliseconds > 10000)
                {
                    _Halted = true;
                }
            }
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
