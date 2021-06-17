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

        ComputerArchitectureAdvancedProject.CommandParser Parser = new ComputerArchitectureAdvancedProject.CommandParser();
        ILayout ADDLayout = new MathLayout(Tokens.ADD);
        ILayout SUBLayout = new MathLayout(Tokens.SUB);
        ILayout MULTLayout = new MathLayout(Tokens.MULT);
        ILayout DIVLayout = new MathLayout(Tokens.DIV);
        ILayout MODLayout = new MathLayout(Tokens.MOD);
        ILayout EQLayout = new MathLayout(Tokens.EQ);

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
    }
}
