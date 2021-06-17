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
        public Tokens Token;

        public PatternBasedLayout(Tokens token, string[] captureGroups)
        {
            Token = token;

            string regexPattern = RegexShortcuts.Start + RegexShortcuts.IgnoreCase;
            regexPattern += token.ToString();
            foreach(string captureGroup in captureGroups)
            {
                regexPattern += RegexShortcuts.Space;
                regexPattern += captureGroup;
            }
            regexPattern += RegexShortcuts.Comment;
            regexPattern += RegexShortcuts.End;

            Pattern = new Regex(regexPattern);
            AssembledBytes = new byte[4];
            AssembledBytes[0] = (byte)token;
        }
    }
}
