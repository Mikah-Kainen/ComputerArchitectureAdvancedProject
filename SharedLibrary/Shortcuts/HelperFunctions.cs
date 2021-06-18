using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Shortcuts
{
    public static class HelperFunctions
    {

        public static string DisassembleMemoryLocation(byte[] input, byte targetIndex)
        {
            string returnString = " 0x";
            string temp;
            for (int i = targetIndex; i < targetIndex + 2; i++)
            {
                //temp = Convert.ToString(input[i], 16);
                temp = input[i].ToString();
                if (temp.Length < 2)
                {
                    returnString += 0;
                }
                returnString += temp;
            }

            return returnString;
        }

        public static void ConvertMemoryLocationToHex(ref byte[] input, byte targetIndex)
        {
            string temp = Convert.ToString(input[1], 16);
            temp += Convert.ToString(input[2], 16);
            input[1] = (byte)(short.Parse(temp) >> 8);
            input[2] = (byte)(short.Parse(temp));
        }

    }
}
