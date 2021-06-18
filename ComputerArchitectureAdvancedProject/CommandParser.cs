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


        public Dictionary<string, short> GotoTracker;

        short currentLocation;
      
        public CommandParser()
        {
            //ADD + (R\d +) +(R\d +) +(R\d +) https://regexr.com/
            //^(?i)(ADD) +(R\d+) +(R\d+) +(R\d+) https://regex101.com/

            GotoTracker = new Dictionary<string, short>();
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
               // if()
            }
        }

        public byte[][] Parse(string[] commands)
        {
            List<byte[]> tempList = new List<byte[]>();
            foreach(string command in commands)
            {
                tempList.Add(Parse(command).ToArray());
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
            byte commandToken = GetToken(command);
            return Dictionaries.GetLayoutFromToken[commandToken].Parse(command);
        }

        public string Parse(byte[] command)
        {
            byte commandToken = (byte)command[0];
            return Dictionaries.GetLayoutFromToken[commandToken].Parse(command);
        }

        public byte GetToken(string input)
        {
            if (Dictionaries.StringToOp.ContainsKey(input))
            {
                return Dictionaries.StringToOp[input];
            }

            List<Regex> superTemp = new List<Regex>();
            foreach (var kvp in Dictionaries.StringToOp)
            {
                Regex current = new Regex(@"^( *(?i)(" + kvp.Key + @"))");
                superTemp.Add(current);
                if (current.IsMatch(input))
                {
                    return kvp.Value;
                }
            }

            Regex isLabel = new Regex(@":$");
            if (isLabel.IsMatch(input))
            {
                GotoTracker.Add(input, currentLocation);
                return Dictionaries.StringToOp["LABEL"];
            }

            return Dictionaries.StringToOp["EMPTY"];
        }

    }
}
