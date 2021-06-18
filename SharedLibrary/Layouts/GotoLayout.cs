using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Layouts
{
    public class GotoLayout : PatternBasedLayout, ILayout
    {

        public static string[] CaptureGroups = new string[]
        {
            RegexShortcuts.Label,
        };

        public GotoLayout(byte opByte)
            : base(opByte, CaptureGroups)
        {

            Dictionaries.GetLayoutFromToken.Add(opByte, this);
        }

        public byte[] Parse(string input)
        {
            string[] parse = Pattern.Split(input);
            if (parse.Length != CaptureGroups.Length + 2)
            {
                throw new SystemException("IDK WHAT THIS COMMAND IS");
            }

            if(parse[1][parse[1].Length - 1] == ':')
            {
                AssembledBytes[1] = 00;
                AssembledBytes[2] = 00;
            }
            else
            {
                ushort temp = ushort.Parse(parse[1]);
                AssembledBytes[1] = (byte)(temp >> 8);
                AssembledBytes[2] = (byte)temp;
            }
            AssembledBytes[3] = 0xff;

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            string returnString = Dictionaries.OpToString[OpByte];

            returnString += " 0x";
            returnString += input[1].ToString();
            returnString += input[2].ToString();

            return returnString;
        }
    }
}
