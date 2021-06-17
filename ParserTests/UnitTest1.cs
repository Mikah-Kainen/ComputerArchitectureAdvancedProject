using ComputerArchitectureAdvancedProject;

using System;
using System.Collections.Generic;
using Xunit;
using SharedLibrary;
using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;

namespace CommandParser.tests
{
    public class UnitTest1
    {
        //Dictionaries.GetLayoutFromToken.Clear();

        static ComputerArchitectureAdvancedProject.CommandParser Parser = new ComputerArchitectureAdvancedProject.CommandParser();
        static ILayout ADDLayout = new MathLayout(Tokens.ADD);
        static ILayout SUBLayout = new MathLayout(Tokens.SUB);
        static ILayout MULTLayout = new MathLayout(Tokens.MULT);
        static ILayout DIVLayout = new MathLayout(Tokens.DIV);
        static ILayout MODLayout = new MathLayout(Tokens.MOD);
        static ILayout EQLayout = new MathLayout(Tokens.EQ);

        [Theory]
        [InlineData("ADD", Tokens.ADD)]
        [InlineData(" ADD", Tokens.ADD)]
        [InlineData("   ADD", Tokens.ADD)]
        [InlineData("adD", Tokens.ADD)]
        [InlineData("eat", Tokens.EMPTY)]
        [InlineData("dad", Tokens.EMPTY)]
        [InlineData("AddSubBurp", Tokens.ADD)]
        [InlineData("BurpAddSub", Tokens.EMPTY)]
        [InlineData("\nADD", Tokens.EMPTY)]
        [InlineData("AS LKBSDKJL123DSFJ:", Tokens.LABEL)]
        [InlineData("DSLFKJSDFFK::::::LSfkLFKJ", Tokens.EMPTY)]

        public void DoesGetTokenWork(string input, Tokens expected)
        {
        
            Assert.Equal(expected, Parser.GetToken(input));
        }


        [Theory]
        [InlineData("Add R01 R02 R02", new byte[4] { 1, 1, 2, 2 })]
        [InlineData("adD r1 r24 r31", new byte[4] { 1, 1, 24, 31 })]
        [InlineData("ADD r1 r3 r2 ;asfdafadf", new byte[4] { 1, 1, 3, 2 })]
        //[InlineData("ADDD r1 r1 r2", new byte[0] { })]

        public void DoesAddParseWork(string command, byte[] expected)
        {
            Tokens commandToken = Parser.GetToken(command);
            byte[] actual = Parser.Parse(command);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("ADDD r1 r1 r2")]

        public void DoesAddParseThrowProperly(string command)
        {
            
            Action shouldThrow = () => { Parser.Parse(command); };
            Assert.Throws<SystemException>(shouldThrow);
        }

        
        [Fact]
        public void DoMathCommandsParseProperly()
        {

            string[] commands =
            {
                "ADD R01 R02 R31",
                "Sub R23 R12 R20 ;haha I will trick the compiler with comments",
                "MUlT R12 R0 R21",
                "div R23 R31 R21",
                "MOD   R23   R12 R30 ; asdlkfj",
                "eq R1 R1 R1",

            };

            var result = Parser.Parse(commands);

            byte[][] expected =
            {
                new byte[4]{ 1, 1, 2, 31},
                new byte[4]{ 2, 23, 12, 20},
                new byte[4]{ 3, 12, 0, 21},
                new byte[4]{ 4, 23, 31, 21},
                new byte[4]{ 5, 23, 12, 30},
                new byte[4]{ 6, 1, 1, 1},
            };

            Assert.Equal(expected, result);
        }

        [Fact]

        public void DoMathCommandsAssembleAndDisassembleCorrectly()
        {
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

            var result = Parser.Parse(commands);
            bool areEqual = true;

            for (int i = 0; i < result.Length; i++)
            {
                for (int x = 0; x < result[i].Length; x++)
                {
                    if (result[i][x] != expected[i][x])
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

            bool areEqual2 = true;
            var result2 = Parser.Parse(result);

            string[] expected2 =
            {
                "ADD R1 R2 R31",
                "SUB R23 R12 R20",
                "MULT R12 R0 R21",
                "DIV R23 R31 R21",
                "MOD R23 R12 R30",
                "EQ R1 R1 R1",
                "ADD R2 R3 R4",
                "ADD R4 R3 R2",
            };

            for (int i = 0; i < result2.Length; i++)
            {
                if (result2[i] != expected2[i])
                {
                    areEqual2 = false;
                }
            }

            Assert.True(areEqual);
            Assert.True(areEqual2);
        }
    }
}
