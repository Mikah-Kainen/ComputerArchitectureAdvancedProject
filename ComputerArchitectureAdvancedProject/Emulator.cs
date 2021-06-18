using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ComputerArchitectureAdvancedProject
{
    public class Emulator
    {
        public const int INSTRUCTION_BYTES = 4;
        static ushort[] Registers = new ushort[32];

        public static byte[] RAM = new byte[256];
        public static Memory<byte> ProgramSpace;
        public static ushort IP = 31;
        public static ushort SP = 30;

        public Span<byte> GetNextInstruction()
        {
            return null;
        }

        public Emulator(byte[] programBytes)
        {
            ushort stackSize = 10;

            Registers[IP] = 0;
            Registers[SP] = stackSize;

            Span<byte> stackSpan = RAM.AsSpan(0, 256);
            programBytes.CopyTo(ProgramSpace);
            ProgramSpace = RAM.AsMemory(0, 256);

            Span<ushort> stackSpace = MemoryMarshal.Cast<byte, ushort>(stackSpan);
        }
    }
}
