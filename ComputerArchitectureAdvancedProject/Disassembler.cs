using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerArchitectureAdvancedProject
{
    public class Disassembler
    {

        public Disassembler()
        {

        }

        public string DisassembleBytes(byte[] command)
        {
            Tokens commandToken = (Tokens)command[0];
            string returnString = commandToken.ToString();

            ILayout commandLayout = (ILayout)Dictionaries.GetLayoutFromToken[commandToken];
            //PatternBasedLayout = 

            return returnString;
        }
    }
}
