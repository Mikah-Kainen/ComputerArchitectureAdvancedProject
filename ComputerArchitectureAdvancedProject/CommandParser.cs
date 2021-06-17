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

            //Type currentType = typeof(ILayout);

            //Type[] allTypes = Assembly.GetAssembly(currentType).GetTypes();
            //Type[] allLayoutTypes = allTypes.Where(x => x.IsSubclassOf(currentType)).ToArray();

            ////BaseInstruction[] allInstructions = allInstructionTypes.Select(x => (BaseInstruction)Activator.CreateInstance(x)).ToArray();


            //getToken = new Dictionary<string, Tokens>();
            //GotoTracker = new Dictionary<string, short>();
            //ParseCommand = new Dictionary<Tokens, Func<string, byte[]>>();

            GotoTracker = new Dictionary<string, short>();
            getToken = new Dictionary<string, Tokens>();
            foreach(int Op in Enum.GetValues(typeof(Tokens)))
            {
                getToken.Add(((Tokens)Op).ToString(), (Tokens)Op);
            }



            //foreach (Type type in allLayoutTypes)
            //{
            //    Tokens currentToken = Dictionaries.GetTokenFromType[type];
            //    ParseCommand.Add(currentToken, (input) =>
            //    {
            //        var temp = (ILayout)Activator.CreateInstance(type);
            //        return temp.Parse(input);
            //    });
            //}
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

        public byte[] Parse(string command)
        {
            Tokens commandToken = GetToken(command);
            //ILayout commandLayout = Dictionaries.GetLayoutFromToken[commandToken];
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
