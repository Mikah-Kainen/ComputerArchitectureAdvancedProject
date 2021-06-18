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
            $"{RegexShortcuts.Label}|{RegexShortcuts.MemoryAddress}",
        };

        public GotoLayout(byte opByte)
            : base(opByte, CaptureGroups)
        {

            Dictionaries.GetLayoutFromOpByte.Add(opByte, this);
        }

        public byte[] Parse(string input)
        {
            var parse = Pattern.Match(input);
            //if (parse.Groups.Count != CaptureGroups.Length + 1)
            //{
            //    throw new SystemException("IDK WHAT THIS COMMAND IS");
            //}

            if(parse.Groups[1].Length > 0)
            {
                ushort loc = Dictionaries.GotoTracker[parse.Groups[1].Value];
                AssembledBytes[1] = (byte)(loc >> 8);
                AssembledBytes[2] = (byte)loc;
            }
            else
            {
                ushort temp = ushort.Parse(parse.Groups[2].Value);
                AssembledBytes[1] = (byte)(temp >> 8);
                AssembledBytes[2] = (byte)temp;
            }
            AssembledBytes[3] = 0xff;

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            string returnString = Dictionaries.OpToString[OpByte];

            //HelperFunctions.ConvertMemoryLocationToHex(ref input, 1);

            returnString += HelperFunctions.DisassembleMemoryLocation(input, 1);

            return returnString;
        }
    }
}
