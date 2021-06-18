using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedLibrary.Layouts
{
    public class MathLayout : PatternBasedLayout, ILayout
    {
        public static string[] CaptureGroups = new string[]
        {
            RegexShortcuts.Register,
            RegexShortcuts.Register,
            RegexShortcuts.Register,
        };
        public MathLayout(byte opByte)
            : base(opByte, CaptureGroups)
        {

              Dictionaries.GetLayoutFromOpByte.Add(opByte, this);
        }

        public byte[] Parse(string input)
        {
            string[] parse = Pattern.Split(input);
            if (parse.Length != CaptureGroups.Length + 2)
            {
                throw new SystemException("IDK WHAT THIS COMMAND IS");
            }

            for (int i = 1; i < AssembledBytes.Length; i++)
            {
                AssembledBytes[i] = byte.Parse(parse[i]);
            }

            return AssembledBytes;
        }

        public string Parse(byte[] input)
        {
            string returnString = Dictionaries.OpToString[OpByte];

            for (int i = 1; i < input.Length; i++)
            {
                returnString += " R";
                returnString += input[i];
            }

            return returnString;
        }
    }
}