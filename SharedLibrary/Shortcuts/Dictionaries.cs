using SharedLibrary.Layouts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Shortcuts
{
    public static class Dictionaries
    {
        //public static Dictionary<Tokens, Func<string, byte[]>> GetParseFuncFromToken = new Dictionary<Tokens, Func<string, byte[]>>()
        //{
        //    //[Tokens.NOP]
        //    //[Tokens.ADD] = typeof(MathLayout),
        //    //[Tokens.SUB] = typeof(MathLayout),
        //    //[Tokens.MULT]
        //    //[Tokens.DIV]
        //    //[Tokens.MOD]
        //    //[Tokens.EQ]
        //    //[Tokens.SETI]
        //    //[Tokens.SET]
        //    //[Tokens.LOAD]
        //    //[Tokens.GOTO]
        //    //[Tokens.GOTR]
        //    //[Tokens.STR]
        //};

        public static Dictionary<byte, ILayout> GetLayoutFromOpByte = new Dictionary<byte, ILayout>();

        public static Dictionary<byte, string> OpToString = new Dictionary<byte, string>();

        public static Dictionary<string, byte> StringToOpDebug = new Dictionary<string, byte>();

        public static Dictionary<string, ushort> GotoTracker = new Dictionary<string, ushort>();
    }
}