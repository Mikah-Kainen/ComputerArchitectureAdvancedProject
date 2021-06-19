using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screen
{
    public partial class Form1 : Form
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 32;
        public const int SCALE = 5;
        public Color ChosenColor = Color.YellowGreen;

        public Form1()
        {
            InitializeComponent();
            Bitmap image;
            var openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog.FileName;
                image = (Bitmap)Image.FromFile(file);
            }

            //Fill.Click += Fill_Click;
            //ChooseColor.Click += ChooseColor_Click;
            ComputerArchitectureAdvancedProject.Program.InitializeCommands();

            ComputerArchitectureAdvancedProject.CommandParser Parser = new ComputerArchitectureAdvancedProject.CommandParser();

            string[] commands = {
            "SETI R1 01",
            "SETI R2 02",
            "SETI R3 03",
            "SETI R4 05",
            //"PLACE:",

            //"ADD R1 R2 R3",
            //"ADD R2 R2 R2",
            //"ADD R3 R2 R2",

            //"EQ R5 R1 R4",
            //"GOBR PLACE: R5",
            "STr r3 0",
            "STR R4 0x0002",
            };

            byte[] Commands = Parser.ParseA(commands);

            ComputerArchitectureAdvancedProject.Emulator emulator = new ComputerArchitectureAdvancedProject.Emulator(Commands);
        }

        private void ChooseColor_Click(object sender, EventArgs e)
        {
            Colors.ShowDialog();
            ChosenColor = Colors.Color;
        }

        private void Fill_Click(object sender, EventArgs e)
        {

            Bitmap bitmap = new Bitmap(WIDTH, HEIGHT);
            for(int x = 0; x < WIDTH; x ++)
            {
                for(int y = 0; y < HEIGHT; y ++)
                {
                    bitmap.SetPixel(x, y, ChosenColor);
                }
            }
            Screen.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(WIDTH * SCALE, HEIGHT * SCALE);
            for(int x = 0; x < WIDTH; x ++)
            {
                for(int y = 0; y < HEIGHT; y ++)
                {
                    for(int x2 = 0; x2 < SCALE; x2 ++)
                    {
                        for(int y2 = 0; y2 < SCALE; y2 ++)
                        {
                            bitmap.SetPixel(x * SCALE + x2, y * SCALE + y2, ChosenColor);
                        }
                    }
                }
            }


            ScaledScreen.Image = bitmap;
        }

    }
}
