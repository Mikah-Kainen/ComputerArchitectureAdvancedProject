
using SharedLibrary;
using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;
using System;
using System.Text.RegularExpressions;

namespace ComputerArchitectureAdvancedProject
{
    class Program
    {
        static void Main(string[] args)
        {
            ILayout ADDLayout = new MathLayout(Tokens.ADD);
            ILayout SUBLayout = new MathLayout(Tokens.SUB);
            ILayout MULTLayout = new MathLayout(Tokens.MULT);
            ILayout DIVLayout = new MathLayout(Tokens.DIV);
            ILayout MODLayout = new MathLayout(Tokens.MOD);
            ILayout EQLayout = new MathLayout(Tokens.EQ);

            string[] commands =
{
                "ADD R01 R02 R31",
                "Sub R23 R12 R20 ;haha I will trick the compiler with comments",
                "MUlT R12 R0 R21",
                "div R23 R31 R21",
                "MOD   R23   R12 R30 ; asdlkfj",
                "eq R1 R1 R1",
                "Add R2 R3 R4",
                "Add R4 R3 R2",
            };

            byte[][] expected =
            {
                new byte[4]{ 1, 1, 2, 31},
                new byte[4]{ 2, 23, 12, 20},
                new byte[4]{ 3, 12, 0, 21},
                new byte[4]{ 4, 23, 31, 21},
                new byte[4]{ 5, 23, 12, 30},
                new byte[4]{ 6, 1, 1, 1},
                new byte[4]{ 1, 2, 3, 4},
                new byte[4]{ 1, 4, 3, 2},
            };

            CommandParser Parser = new CommandParser();
            var result = Parser.Parse(commands);
            bool areEqual = true;

            for(int i = 0; i < result.Length; i ++)
            {
                for(int x = 0; x < result[i].Length; x ++)
                {
                    if(result[i][x] != expected[i][x])
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

        }
    }
    
}
