using SharedLibrary.Shortcuts;

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Layouts
{
    public class MemoryLayout : PatternBasedLayout, ILayout
    {
        public static string[] CaptureGroups =
        {
            RegexShortcuts.Register,
            RegexShortcuts.MemoryAddress
        };

        public MemoryLayout(Tokens token)
            : base(token, CaptureGroups)
        {
            Dictionaries.GetLayoutFromToken.Add(token, this);
        }

        public byte[] Parse(string input)
        {
            string[] parse = Pattern.Split(input);
            if (parse.Length != CaptureGroups.Length + 2)
            {
                throw new SystemException("IDK WHAT THIS COMMAND IS");
            }

            AssembledBytes[1] = byte.Parse(parse[1]);
            AssembledBytes[2] = (byte)(short.Parse(parse[2]) >> 8);
            AssembledBytes[3] = byte.Parse(parse[2]);

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            string returnString = Dictionaries.TokenToString[Token];

            returnString += " R";
            returnString += input[1];

            returnString += " 0x";
            returnString += input[2];
            returnString += input[3];

            return returnString;
        }
    }
}
