using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ComputerArchitectureAdvancedProject
{
    public class Emulator
    {
        public static Dictionary<byte, Action<byte[]>> OpToActions = new Dictionary<byte, Action<byte[]>>()
        {
            //[0x00] = new Action<byte[]>(),
            [0x01] = new Action<byte[]>((inputs) => { Registers[inputs[1]] = (ushort)(Registers[inputs[2]] + Registers[inputs[3]]); }),
            //[0x02] = new Action<byte[]>(),
            //[0x03] = new Action<byte[]>(),
            //[0x04] = new Action<byte[]>(),
            //[0x05] = new Action<byte[]>(),
            [0x06] = new Action<byte[]>((inputs) => { Registers[inputs[1]] = Registers[inputs[2]] == Registers[inputs[3]] ? (ushort)1 : (ushort)0; }),
            [0x010] = new Action<byte[]>((inputs) => { Registers[IP] = (ushort)((inputs[1] << 8) + inputs[2]); }),
            //[0x11] = new Action<byte[]>(),
            [0x20] = new Action<byte[]>((inputs) => { Registers[inputs[1]] = (ushort)((inputs[2] << 8) + inputs[3]); }),
            [0x21] = new Action<byte[]>((inputs) => { Registers[inputs[1]] = Registers[inputs[2]]; }),
        };                     

        public const int INSTRUCTION_BYTES = 4;
        static ushort[] Registers = new ushort[32];

        public static byte[] RAM = new byte[256];
        public static Memory<byte> ProgramSpace;
        public static ushort IP = 31;
        public static ushort SP = 30;

        public Span<byte> GetNextInstruction()
        {
            Span<byte> returnValue = ProgramSpace.Slice(Registers[IP], 4).Span;
            Registers[IP] += 4;
            return returnValue;
        }

        public Emulator(byte[] programBytes)
        {
            programBytes = new byte[] { 
                0x20, 0x01, 0x00, 0x01,
                0x20, 0x02, 0x00, 0x02,
                0x20, 0x03, 0x00, 0x03,

                0x06, 0x04, 0x01, 0x01,

                0x01, 0x01, 0x02, 0x03, 
                0x01, 0x02, 0x02, 0x02, 
                0x01, 0x03, 0x02, 0x02,
                
                0x10, 0x00, 0xC, 0xff};


            ushort stackSize = 10;
            ushort stackStart = 0xC0;

            Registers[IP] = 0;
            Registers[SP] = stackSize;

            Span<byte> stackSpan = RAM.AsSpan(stackStart - stackSize + 1, stackSize);
            ProgramSpace = RAM.AsMemory(stackStart, RAM.Length - stackStart);
            programBytes.CopyTo(ProgramSpace);

            EmulateAllInstructions();

            //Span<ushort> stackSpace = MemoryMarshal.Cast<byte, ushort>(stackSpan);
        }


        public bool EmulateNextInstruction()
        {
            Span<byte> currentInstruction = GetNextInstruction();
            //var opCode = currentInstruction[0];
            if (OpToActions.ContainsKey(currentInstruction[0]))
            {
                OpToActions[currentInstruction[0]](currentInstruction.ToArray());
                return true;
            }
            return false;
        }

        public void EmulateAllInstructions()
        {
            while (Registers[IP] < ProgramSpace.Length && EmulateNextInstruction());
        }
    }
}
