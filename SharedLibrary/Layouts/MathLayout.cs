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
        static string[] captureGroups = new string[]
        {
            RegexShortcuts.register,
            RegexShortcuts.register,
            RegexShortcuts.register,
        };
        public MathLayout(Tokens token)
            : base(token, captureGroups)
        {
            AssembledBytes[0] = (byte)token; // gets the OpByte and stores it in AssembledBytes

            //if (!Dictionaries.GetParseFuncFromToken.ContainsKey(token))
            //{
                Dictionaries.GetLayoutFromToken.Add(token, this);
            //}
        }


        public byte[] Parse(string input)
        {
            string[] parse = Pattern.Split(input);
            if (parse.Length != AssembledBytes.Length + 1)
            {
                throw new SystemException("IDK WHAT THIS COMMAND IS");
            }

            for (int i = 1; i < AssembledBytes.Length; i++)
            {
                AssembledBytes[i] = byte.Parse(parse[i]);
            }

            return AssembledBytes;
        }
    }
}