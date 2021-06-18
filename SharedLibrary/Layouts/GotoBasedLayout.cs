using SharedLibrary.Shortcuts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Layouts
{
    public class GotoBasedLayout : PatternBasedLayout
    {
        public static string GotoCaptureGroup = $"{RegexShortcuts.Label}|{RegexShortcuts.MemoryAddress}|{RegexShortcuts.Register}";
        public static string[] CreateCaptureGroups(string[] endCaptureGroups)
        {
            string[] returnValue = new string[endCaptureGroups.Length + 1];
            returnValue[0] = GotoCaptureGroup;
            for(int i = 0; i < endCaptureGroups.Length; i ++)
            {
                returnValue[i + 1] = endCaptureGroups[i];
            }
            return returnValue;
        }
        public GotoBasedLayout(byte opByte, string[] endCaptureGroups)
            : base(opByte, CreateCaptureGroups(endCaptureGroups))
        {
        }
    }
}
