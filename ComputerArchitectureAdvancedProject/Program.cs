
using SharedLibrary;
using SharedLibrary.Layouts;
using SharedLibrary.Shortcuts;
using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Reflection;
using System.Linq;

namespace ComputerArchitectureAdvancedProject
{
    class Program
    {
        class Model
        {
            public string OpName { get; set; }
            public string LayoutName { get; set; }
            public string OpCode { get; set; }
        }

        public static void InitializeCommands()
        {
            string filePath = "Test2.json";

            //Model addModel = new Model() { OpName = "ADD", LayoutName = "MathLayout", OpCode = "01" };
            //string serializedData = JsonConvert.SerializeObject(addModel);
            //System.IO.File.WriteAllText(filePath, serializedData);

            

            Model[] returnedModels = JsonConvert.DeserializeObject<Model[]>(System.IO.File.ReadAllText(filePath));

            Type[] allTypes = Assembly.GetAssembly(typeof(ILayout)).GetTypes();
            Type[] allILayoutTypes = allTypes.Where(new Func<Type, bool>((type) =>
            {
                var interfaces = type.GetInterfaces();
                foreach(var @interface in interfaces)
                { 
                    if(@interface == typeof(ILayout))
                    {
                        return true;
                    }
                }
                return false;
            })).ToArray();

            //Type[] allLayoutTypes = allTypes.Where(x => x.GetInterface("ILayout") != null).ToArray();

            foreach (Model returnedModel in returnedModels)
            {
                foreach (Type type in allILayoutTypes)
                {
                    if (type.Name == returnedModel.LayoutName)
                    {
                        var @byte = byte.Parse(returnedModel.OpCode, System.Globalization.NumberStyles.HexNumber);

                        Dictionaries.StringToOp.Add($"{returnedModel.OpName}", @byte);
                        Dictionaries.OpToString.Add(@byte, $"{returnedModel.OpName}");

                        Activator.CreateInstance(type, @byte);
                    }
                }
            }

            //ILayout ADDLayout = new MathLayout(Tokens.ADD);
            //ILayout SUBLayout = new MathLayout(Tokens.SUB);
            //ILayout MULTLayout = new MathLayout(Tokens.MULT);
            //ILayout DIVLayout = new MathLayout(Tokens.DIV);
            //ILayout MODLayout = new MathLayout(Tokens.MOD);
            //ILayout EQLayout = new MathLayout(Tokens.EQ);

            //ILayout SETLayout = new MemoryLayout(Tokens.SET);
            //ILayout LOADLayout = new MemoryLayout(Tokens.LOAD);
        }

        static void Main(string[] args)
        {

            InitializeCommands();


            string[] commands =
            {
                "SET R01 0x0023",
                //"Sub R23 R12 R20 ;haha I will trick the compiler with comments",
                //"MUlT R12 R0 R21",
                //"div R23 R31 R21",
                //"MOD   R23   R12 R30 ; asdlkfj",
                "eq R1 R1 R1",
                "Add R2 R3 R4",
                "Add R4 R3 R2",
                "GOTO LABEL:",
            };

            byte[][] expected =
            {
                new byte[4]{ 0x21, 1, 00, 23},
                //new byte[4]{ 2, 23, 12, 20},
                //new byte[4]{ 3, 12, 0, 21},
                //new byte[4]{ 4, 23, 31, 21},
                //new byte[4]{ 5, 23, 12, 30},
                new byte[4]{ 6, 1, 1, 1},
                new byte[4]{ 1, 2, 3, 4},
                new byte[4]{ 1, 4, 3, 2},
                new byte[4]{ 0x10, 00, 00, 0xff},
            };

            CommandParser Parser = new CommandParser();
            var result = Parser.Parse(commands);
            bool areEqual = true;

            for(int i = 0; i < result.Length; i ++)
            {
                for(int x = 0; x < result[i].Length; x ++)
                {
                    if(result[i][x] != expected[i][x])
                    {
                        areEqual = false;
                        break;
                    }
                }
            }

            bool areEqual2 = true;
            var result2 = Parser.Parse(result);

            string[] expected2 =
            {
                "SET R1 0x023",
                //"SUB R23 R12 R20",
                //"MULT R12 R0 R21",
                //"DIV R23 R31 R21",
                //"MOD R23 R12 R30",
                "EQ R1 R1 R1",
                "ADD R2 R3 R4",
                "ADD R4 R3 R2",
                "GOTO 0x00",
            };

            for(int i = 0; i < result2.Length; i ++)
            {
                if(result2[i] != expected2[i])
                {
                    areEqual2 = false;
                }
            }


        }
   
    }
    
}
