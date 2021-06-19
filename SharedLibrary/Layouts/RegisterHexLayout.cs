using SharedLibrary.Shortcuts;

using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Layouts
{
    class RegisterHexLayout : PatternBasedLayout, ILayout
    {

        public static string[] CaptureGroups = new string[]
        {
            RegexShortcuts.Register,
            RegexShortcuts.HexValue,
        };

        public RegisterHexLayout(byte opByte)
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
            AssembledBytes[1] = byte.Parse(parse.Groups[1].Value);

            ushort temp = ushort.Parse(parse.Groups[2].Value);
            AssembledBytes[2] = (byte)(temp >> 8);
            AssembledBytes[3] = (byte)temp;

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            throw new NotImplementedException();
        }
    }
}
