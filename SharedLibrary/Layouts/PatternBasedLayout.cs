using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedLibrary.Layouts
{
    public class PatternBasedLayout : ILayout
    {
        public Regex Pattern;
        public byte[] AssembledBytes;

        public PatternBasedLayout(Tokens token, string[] captureGroups)
        {
            string regexPattern = "{RegexShortcuts.start}{RegexShortcuts.ignoreCase}";
            regexPattern += token.ToString();
            foreach(string captureGroup in captureGroups)
            {
                regexPattern += RegexShortcuts.space;
                regexPattern += captureGroup;
            }
            if(captureGroups.Length < 3)
            {
                regexPattern += RegexShortcuts.space;
                regexPattern += RegexShortcuts.padding;
            }
            regexPattern += RegexShortcuts.comment;
            regexPattern += RegexShortcuts.end;

            Pattern = new Regex(regexPattern);
            AssembledBytes = new byte[captureGroups.Length];
            AssembledBytes[0] = (byte)token;

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
