using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelyGoodCars
{
    internal class Styling
    {
        public static int LineLength = 50;
        public static int ProgressBarWidth = 20;

        internal static void AddHeader(string text)
        {
            WriteLineInColor(text, ConsoleColor.DarkYellow);
        }
        internal static void AddLine()
        {
            WriteLineInColor(new string('-', LineLength), ConsoleColor.Cyan);
        }

        internal static void AddOption(string text)
        {
            WriteLineInColor(text, ConsoleColor.Yellow);
        }

        internal static void AddAction(string text)
        {
            WriteLineInColor(text, ConsoleColor.Green);
        }

        internal static void AddInfo(string text)
        {
            WriteLineInColor(text, ConsoleColor.Blue);
        }

        internal static void AddError(string text)
        {
            WriteLineInColor(text, ConsoleColor.Red);
        }

        internal static void Skip()
        {
            Console.WriteLine();
        }

        internal static void WriteLine(string text)
        {
            WriteLineInColor(text, ConsoleColor.White);
        }

        internal static void Write(string text)
        {
            WriteInColor(text, ConsoleColor.White);
        }

        internal static void WriteLineInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        internal static void WriteInColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        internal static void ProgressBar(decimal total, decimal progress, bool showPercentage)
        {
            int maxBarWidth = ProgressBarWidth; // aantal vakjes
            decimal percentage = Math.Round(100 / total * progress, 1);
            decimal barWidth = Math.Floor(maxBarWidth / total * progress);

            Styling.WriteInColor("[", ConsoleColor.White);
            for (int i = 0; i < maxBarWidth; i++)
            {
                if (i < barWidth)
                {
                    Styling.WriteInColor("■", ConsoleColor.Green);
                }
                else
                {
                    Styling.WriteInColor("□", ConsoleColor.White);
                }
            }
            Styling.WriteInColor("]", ConsoleColor.White);

            if (showPercentage)
            {
                Styling.WriteInColor($" {percentage}%", ConsoleColor.White);
            }

            Styling.Skip(); // Skip line voor styling
        }
    }
}
