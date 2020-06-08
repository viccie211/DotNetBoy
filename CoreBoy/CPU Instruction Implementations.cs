using System;
using System.Collections.Generic;

namespace CoreBoy
{
    public partial class CPU
    {
        #region Instructions
        #region ADD
        private static void ADD_A_B(CPU cpu)
        {
            Add(ref cpu._RegA, ref cpu._RegB, ref cpu._RegF, ref cpu._RegPC);
        }

        private static void ADD_A_C(CPU cpu)
        {
            Add(ref cpu._RegA, ref cpu._RegC, ref cpu._RegF, ref cpu._RegPC);
        }

        private static void ADD_A_D(CPU cpu)
        {
            Add(ref cpu._RegA, ref cpu._RegD, ref cpu._RegF, ref cpu._RegPC);
        }

        private static void ADD_A_E(CPU cpu)
        {
            Add(ref cpu._RegA, ref cpu._RegE, ref cpu._RegF, ref cpu._RegPC);
        }

        private static void ADD_A_H(CPU cpu)
        {
            Add(ref cpu._RegA, ref cpu._RegH, ref cpu._RegF, ref cpu._RegPC);
        }
        #endregion

        #region JP
        private static void JP(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, true);
        }

        private static void JP_NZ(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, !cpu._RegF.Zero);
        }

        private static void JP_Z(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, cpu._RegF.Zero);
        }

        private static void JP_NC(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, !cpu._RegF.Carry);
        }
        private static void JP_C(CPU cpu)
        {
            Jump(ref cpu._RegPC, cpu._MMU, cpu._RegF.Carry);
        }
        #endregion

        #region LD
        #region LD_A
        public static void LD_A_A(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LOAD_A_B(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_A_C(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_A_D(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_A_E(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_A_H(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_A_L(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_A_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegA, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_A_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegA, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_B
        public static void LD_B_A(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_B_B(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_B_C(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_B_D(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_B_E(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_B_H(CPU cpu)
        {
            LoadByte(ref cpu._RegB, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_B_L(CPU cpu)
        {
            LoadByte(ref cpu._RegA, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_B_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegB, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }
        public static void LD_B_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegB, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_C
        public static void LD_C_A(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_C_B(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_C_C(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_C_D(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_C_E(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_C_H(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_C_L(CPU cpu)
        {
            LoadByte(ref cpu._RegC, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_C_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegC, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_C_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegC, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_D
        public static void LD_D_A(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_D_B(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_D_C(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_D_D(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_D_E(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_D_H(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_D_L(CPU cpu)
        {
            LoadByte(ref cpu._RegD, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_D_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegD, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }


        public static void LD_D_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegD, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_E
        public static void LD_E_A(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_E_B(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_E_C(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_E_D(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_E_E(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_E_H(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_E_L(CPU cpu)
        {
            LoadByte(ref cpu._RegE, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_E_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegE, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_E_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegE, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_H
        public static void LD_H_A(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_H_B(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_H_C(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_H_D(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_H_E(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_H_H(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_H_L(CPU cpu)
        {
            LoadByte(ref cpu._RegH, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_H_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegH, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_H_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegH, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_L
        public static void LD_L_A(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegA);
            cpu._RegPC++;
        }

        public static void LD_L_B(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegB);
            cpu._RegPC++;
        }

        public static void LD_L_C(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegC);
            cpu._RegPC++;
        }

        public static void LD_L_D(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegD);
            cpu._RegPC++;
        }

        public static void LD_L_E(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegE);
            cpu._RegPC++;
        }

        public static void LD_L_H(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegH);
            cpu._RegPC++;
        }

        public static void LD_L_L(CPU cpu)
        {
            LoadByte(ref cpu._RegL, cpu._RegL);
            cpu._RegPC++;
        }

        public static void LD_L_HL(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegL, cpu._RegHL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_L_D8(CPU cpu)
        {
            LoadFromAddress(ref cpu._RegL, (ushort)(cpu._RegPC + 1), cpu._MMU);
            cpu._RegPC += 2;
        }
        #endregion

        #region LD_HL
        public static void LD_HL_A(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegA, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_B(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegB, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_C(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegC, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_D(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegD, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_E(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegE, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_H(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegH, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_L(CPU cpu)
        {
            LoadByteToAddress(cpu._RegHL, cpu._RegL, cpu._MMU);
            cpu._RegPC++;
        }

        public static void LD_HL_D8(CPU cpu)
        {
            byte value = 0;
            LoadFromAddress(ref value, (ushort)(cpu._RegPC + 1), cpu._MMU);
            LoadByteToAddress(cpu._RegHL, value, cpu._MMU);
            cpu._RegPC += 2;
        }

        #endregion
        #endregion

        #region PUSH
        public static void PUSH_
        #endregion

        #endregion

        #region SubInstructions
        private static void Add(ref byte target, ref byte source, ref FlagsRegister flagsRegister, ref ushort programCounter)
        {
            int newValue = target + source;
            byte result = (byte)(newValue % 256);

            if (result == 0)
            {
                flagsRegister.Zero = true;
            }

            flagsRegister.Subtract = false;
            flagsRegister.Carry = newValue > 255;
            flagsRegister.HalfCarry = (target & 0xF) + (source & 0xF) > 0xF;
            target = result;
            programCounter++;
        }

        private static void Jump(ref ushort programCounter, MMU mmu, bool conditionMet)
        {
            if (!conditionMet)
            {
                programCounter += 3;
                return;
            }
            ushort newAddress = mmu.ReadWordLSFirst((ushort)(programCounter + 1));
            programCounter = newAddress;
        }


        private static void LoadFromAddress(ref byte target, ushort address, MMU mmu)
        {
            LoadByte(ref target, mmu.ReadByte(address));
        }
        private static void LoadByte(ref byte target, byte source)
        {
            target = source;
        }

        private static void LoadByteToAddress(ushort targetAddress, byte source, MMU mmu)
        {
            mmu.WriteByte(targetAddress, source);
        }

        private static void Push(ushort value, CPU cpu)
        {
            cpu._RegSP--;
            LoadByteToAddress(cpu._RegSP, ByteUshortHelper.UpperByteOfSixteenBits(value), cpu._MMU);
            cpu._RegSP--;
            LoadByteToAddress(cpu._RegSP, ByteUshortHelper.LowerByteOfSixteenBits(value), cpu._MMU);
            cpu._RegPC++;
        }
        #endregion
    }

}