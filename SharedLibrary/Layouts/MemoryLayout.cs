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

        public MemoryLayout(byte token)
            : base(token, CaptureGroups)
        {
            Dictionaries.GetLayoutFromOpByte.Add(token, this);
        }

        public byte[] Parse(string input)
        {
            string[] parse = Pattern.Split(input);
            if (parse.Length != CaptureGroups.Length + 2)
            {
                throw new SystemException("IDK WHAT THIS COMMAND IS");
            }

            AssembledBytes[1] = byte.Parse(parse[1]);
            ushort temp = ushort.Parse(parse[2], System.Globalization.NumberStyles.HexNumber);
            AssembledBytes[2] = (byte)(temp >> 8);
            AssembledBytes[3] = (byte)temp;

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            string returnString = Dictionaries.OpToString[OpByte];

            returnString += " R";
            returnString += input[1];

            returnString += HelperFunctions.DisassembleMemoryLocation(input, 2);

            return returnString;
        }
    }
}
