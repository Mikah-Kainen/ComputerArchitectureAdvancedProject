using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Shortcuts
{
    public static class RegexShortcuts
    {
        public static string space = " +";
        public static string ignoreCase = "(?i)";
        public static string start = "^";
        public static string end = "$";
        public static string comment = "(?:;*.*)";
        public static string register = @"R([0-2][0-9]|3[0-1]|[0-9])";
        public static string hexValue = @"(?:0x)?(\d{0,2})";
        public static string padding = @"(?:0x)?f{0,4}";
        public static string memoryAddress = @"(?:0x)?(\d{0,4})";
    }

}

