using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedLibrary.Layouts
{
    public class PatternBasedLayout
    {
        public Regex Pattern;
        public byte[] AssembledBytes;
        public byte OpByte;

        public PatternBasedLayout(byte opByte, string[] captureGroups)
        {
            OpByte = opByte;

            string regexPattern = RegexShortcuts.Start + RegexShortcuts.IgnoreCase;
            regexPattern += Dictionaries.OpToString[opByte];
            foreach(string captureGroup in captureGroups)
            {
                regexPattern += RegexShortcuts.Space;
                regexPattern += captureGroup;
            }
            regexPattern += RegexShortcuts.Comment;
            regexPattern += RegexShortcuts.End;

            Pattern = new Regex(regexPattern);
            AssembledBytes = new byte[4];
            AssembledBytes[0] = (byte)opByte;
        }
    }
}
