using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedLibrary.Layouts
{
    public interface ILayout
    {
        byte[] Parse(string input);
    }
}
