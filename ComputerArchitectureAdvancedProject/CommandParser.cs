using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SharedLibrary;
using System.Reflection;
using System.Linq;
using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;

namespace ComputerArchitectureAdvancedProject
{

    public class CommandParser
    {
        ushort startLocation;
        ushort currentLocation;
      
        public CommandParser()
        {
            //ADD + (R\d +) +(R\d +) +(R\d +) https://regexr.com/
            //^(?i)(ADD) +(R\d+) +(R\d+) +(R\d+) https://regex101.com/

            startLocation = 0x000;
            currentLocation = 0;
        }

        public string[] SplitCommands(string input)
        {
            string[] commands = input.Split('\n');

            return commands;
        }

        public void FirstPass(string[] commands)
        {
            Regex labelRegex = new Regex(RegexShortcuts.Label);
            foreach (string command in commands)
            {
                string temp = labelRegex.Match(command).Value;
                if (temp != "")
                {
                    Dictionaries.GotoTracker.Add(temp, currentLocation);
                }
                else
                {
                    currentLocation += 4;
                }
            }
        }

        public byte[] ParseA(string[] commands)
        {
            FirstPass(commands);

            List<byte> tempList = new List<byte>();
            foreach (string command in commands)
            {
                if (Dictionaries.GetLayoutFromOpByte.ContainsKey(GetOpByte(command)))
                {
                    foreach(byte part in Parse(command))
                    {
                        tempList.Add(part);
                    }
                }
            }
            return tempList.ToArray();
        }

        public byte[][] Parse(string[] commands)
        {
            FirstPass(commands);

            List<byte[]> tempList = new List<byte[]>();
            foreach(string command in commands)
            {
                if (Dictionaries.GetLayoutFromOpByte.ContainsKey(GetOpByte(command)))
                {
                    tempList.Add(Parse(command).ToArray());
                }
            }
            return tempList.ToArray();
        }

        public string[] Parse(byte[][] commands)
        {
            List<string> tempList = new List<string>();
            foreach (byte[] command in commands)
            {
                tempList.Add(Parse(command).ToString());
            }
            return tempList.ToArray();
        }

        public byte[] Parse(string command)
        {
            byte commandToken = GetOpByte(command);
            return Dictionaries.GetLayoutFromOpByte[commandToken].Parse(command);
        }

        public string Parse(byte[] command)
        {
            byte commandToken = command[0];
            return Dictionaries.GetLayoutFromOpByte[commandToken].Parse(command);
        }

        public byte GetOpByte(string input)
        {
            foreach (var kvp in Dictionaries.OpToString)
            {
                Regex current = new Regex(@"^( *(?i)(" + kvp.Value + @"))");
                if (current.IsMatch(input))
                {
                    return kvp.Key;
                }
            }

            return 0;
        }

    }
}
