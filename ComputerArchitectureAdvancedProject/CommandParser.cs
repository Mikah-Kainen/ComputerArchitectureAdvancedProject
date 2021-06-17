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

        Dictionary<string, Tokens> getToken;

        short currentLocation;
        int totalTokens => (int)Tokens.EMPTY + 1;

        public CommandParser()
        {
            //ADD + (R\d +) +(R\d +) +(R\d +) https://regexr.com/
            //^(?i)(ADD) +(R\d+) +(R\d+) +(R\d+) https://regex101.com/

            GotoTracker = new Dictionary<string, short>();
            getToken = new Dictionary<string, Tokens>();
            foreach(byte Op in Enum.GetValues(typeof(Tokens)))
            {
                string temp = ((Tokens)Op).ToString();
                getToken.Add(temp, (Tokens)Op);
                Dictionaries.TokenToString.Add((Tokens)Op, temp);
            }
             
        }

        public string[] SplitCommands(string input)
        {
            string[] commands = input.Split('\n');

            return commands;
        }

        public void FirstPass(string[] commands)
        {
            Regex labelRegex = new Regex("(.*:)");
            foreach (string command in commands)
            {
                string temp = labelRegex.Match(command).Value;
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
            Tokens commandToken = GetToken(command);
            return Dictionaries.GetLayoutFromToken[commandToken].Parse(command);
        }

        public string Parse(byte[] command)
        {
            Tokens commandToken = (Tokens)command[0];
            return Dictionaries.GetLayoutFromToken[commandToken].Parse(command);
        }

        public Tokens GetToken(string input)
        {
            if (getToken.ContainsKey(input))
            {
                return getToken[input];
            }

            List<Regex> superTemp = new List<Regex>();
            foreach (var kvp in getToken)
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
                return Tokens.LABEL;
            }

            return Tokens.EMPTY;
        }

    }
}
