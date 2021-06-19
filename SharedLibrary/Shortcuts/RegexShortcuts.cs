using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Shortcuts
{
    public static class RegexShortcuts
    {
        public static string Space = " +";
        public static string IgnoreCase = "(?i)";
        public static string Start = "^";
        public static string End = "$";
        public static string Comment = "(?:;*.*)";
        public static string Register = @"R([0-2][0-9]|3[0-1]|[0-9])";
        public static string HexValue = @"(?:0x)?([0-f]{1,4})";
        public static string Padding = @"(?:0x)?f{0,4}";
        public static string MemoryAddress = @"(?:0x)?([0-f]{1,4})";
        public static string Label = @"(.+:+)";
    }

}

