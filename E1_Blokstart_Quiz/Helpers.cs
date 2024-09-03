using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using WheelyGoodCars;

namespace WheelyGoodCars
{
    internal class Helpers
    {
        

        public static string? Ask(string question)
        {
            Styling.WriteInColor(question, ConsoleColor.Gray);
            return Console.ReadLine();
        }

        public static string AskNotEmpty(string question)
        {
            string? retVal = null;
            do
            {
                retVal = Ask(question);
            }
            while (retVal == null || retVal == "");

            return retVal;
        }

        internal static int AskInt(string question)
        {
            bool isInt = false;
            int result = 0;
            while (!isInt)
            {
                string? userInput = Ask(question);
                isInt = int.TryParse(userInput, out result);
            }

            return result;
        }

        internal static float AskFloat(string question)
        {
            bool isInt = false;
            float result = 0;
            while (!isInt)
            {
                string? userInput = Ask(question);
                isInt = float.TryParse(userInput, out result);
            }

            return result;
        }

        internal static string? Wait()
        {
            return Console.ReadLine();
        }
        internal static string? Wait(string text)
        {
            Styling.WriteLineInColor(text, ConsoleColor.Gray);
            return Console.ReadLine();
        }

        internal static void ClearCurrentLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
