using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Shortcuts
{
    public enum Tokens: byte
    {
        NOP = 0,

        ADD = 0x1,
        SUB = 0x2,
        MULT = 0x3,
        DIV = 0x4,
        MOD = 0x5,
        EQ = 0x6,

        LABEL = 0x51,
        COMMENT = 0x52,

        GOTO = 0x10,
        GOTR = 0x11,

        SETI = 0x20,
        SET = 0x21,
        LOAD = 0x22,
        STR = 0x23,

        EMPTY = 0x53,
    }

    //    ["nop"] = 0x00,

    //    ["add"] = 0x01,
    //    ["sub"] = 0x02,
    //    ["mul"] = 0x03,
    //    ["div"] = 0x04,
    //    ["mod"] = 0x05,
    //    ["eq"] = 0x06,

    //    ["goto"] = 0x10,
    //    ["gotr"] = 0x11,

    //    ["seti"] = 0x20,
    //    ["set"] = 0x21,
    //    ["load"] = 0x22,
    //    ["str"] = 0x23,
}

