using SharedLibrary.Shortcuts;

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Layouts
{
    class GOBRLayout : PatternBasedLayout, ILayout
    {
        public static string[] CaptureGroups = new string[]
        {
            RegexShortcuts.Label,
            RegexShortcuts.Register,
        };
        public GOBRLayout(byte opByte)
            : base(opByte, CaptureGroups)
        {

            Dictionaries.GetLayoutFromOpByte.Add(opByte, this);
        }

        public byte[] Parse(string input)
        {
            var parse = Pattern.Match(input);

            if (parse.Groups[1].Length > 0)
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
            AssembledBytes[3] = byte.Parse(parse.Groups[2].Value);

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            throw new NotImplementedException();
        }
    }
}
