using System;
using System.IO;
using System.Text;

namespace ClientTCP
{
    class Bot
    {
        #region botname

        public string nameBot = "SC9";

        #endregion

        public void BotCommand()
        {
            #region command

            Console.WriteLine($"[Bot: {nameBot}] - my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.");
            Console.WriteLine($"@bot /math (example) - math calculation.{Environment.NewLine}@bot /reset - reset color text/background.");
            Console.WriteLine($"@bot /clear - clear chat.{Environment.NewLine}@bot /tcolor - change chat text color.");
            Console.WriteLine($"@bot /bcolor - change chat background color.{Environment.NewLine}@bot /rnd - random number in the specified range.");
            Console.WriteLine($"@bot /setname - name changes.");

            #endregion
        }

        public void BotMath()
        {
            #region math
            Console.WriteLine("select math function to be calculated:");

            string MathOperator = Console.ReadLine();

            Console.WriteLine("Enter the two numbers you want to calculate:");

            try
            {
                string a = Console.ReadLine();
                string b = Console.ReadLine();

                int num1 = Convert.ToInt32(a);
                int num2 = Convert.ToInt32(b);

                switch (MathOperator)
                {
                    case "+":
                        {
                            Console.WriteLine($"[{MathOperator}] Calculation result: {num1 + num2}");
                        }
                        break;
                    case "-":
                        {
                            Console.WriteLine($"[{MathOperator}] Calculation result: {num1 - num2}");
                        }
                        break;
                    case "*":
                        {
                            Console.WriteLine($"[{MathOperator}] Calculation result: {num1 * num2}");
                        }
                        break;
                    case "/":
                        {
                            Console.WriteLine($"[{MathOperator}] Calculation result: {num1 / num2}");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("I don’t know such a mathematical operator (");
                        }
                        break;
                }

                Console.WriteLine($"[Bot: {nameBot}] - Bot completed the robot with computations");

            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }
        public void BotRandom()
        {
            #region random
            string error = "[Error] ";

            Console.WriteLine("Enter a number from:");
            string a = Console.ReadLine();

            Console.WriteLine("Enter number to:");
            string b = Console.ReadLine();

            try
            {
                int x = Convert.ToInt32(a);
                int y = Convert.ToInt32(b);

                var rnd = new Random();

                if (x > y)
                {
                    Console.WriteLine(error + "Number from, greater than number to.");
                }
                if (x == y)
                {
                    Console.WriteLine(error + "You entered two identical numbers to get a random number... trolling???");
                }
                if (x < y)
                {
                    var result = rnd.Next(x, y);
                    Console.WriteLine($"Random number ranging from {x} and to {y}: {result}.");
                }

                Console.WriteLine($"[Bot: {nameBot}] - Random number regeneration completed");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }
        public void BotTextColor()
        {
            #region textcolor
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("         Available colors for changing chat text color");
            Console.WriteLine("----------------------------------------------------------------");

            if (Console.ForegroundColor != ConsoleColor.White)
            {
                Console.WriteLine("The background / chat color will turn white at the time of selection");
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Red");
            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Cyan");
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Yellow");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Magenta");
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Green");
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("Blue");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter the names of one of the colors you want to choose");
            string Select = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(Select))
            {
                for (var i = 0; i == 0;)
                {
                    Console.WriteLine("Color cannot be empty, try entering again.");

                    string ColorRepeat = Console.ReadLine();
                    Select = ColorRepeat;

                    if (ColorRepeat != "")
                    {
                        Select = ColorRepeat;
                        break;
                    }
                }
            }

            if (Select.Equals("Red", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (Select.Equals("Cyan", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            if (Select.Equals("Yellow", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (Select.Equals("Magenta", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            if (Select.Equals("Green", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            if (Select.Equals("Blue", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.WriteLine($"[Bot: {nameBot}] - Chat text color changed");
            #endregion
        }
        public void BotBackgroundColor()
        {
            #region backgroundcolor

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("         Available colors for changing chat background:");
            Console.WriteLine("----------------------------------------------------------------");

            if (Console.BackgroundColor != ConsoleColor.White)
            {
                Console.WriteLine("The background / chat color will turn white at the time of selection");
            }

            Console.BackgroundColor = ConsoleColor.Black;

            Console.BackgroundColor = ConsoleColor.Red; Console.WriteLine("Red");
            Console.BackgroundColor = ConsoleColor.Cyan; Console.WriteLine("Cyan");
            Console.BackgroundColor = ConsoleColor.Yellow; Console.WriteLine("Yellow");
            Console.BackgroundColor = ConsoleColor.Magenta; Console.WriteLine("Magenta");
            Console.BackgroundColor = ConsoleColor.Green; Console.WriteLine("Green");
            Console.BackgroundColor = ConsoleColor.Blue; Console.WriteLine("Blue");

            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine("Enter the names of one of the colors you want to choose");
            string Select = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(Select))
            {
                for (var i = 0; i == 0;)
                {
                    Console.WriteLine("Color cannot be empty, try entering again.");

                    string ColorRepeat = Console.ReadLine();
                    Select = ColorRepeat;

                    if (ColorRepeat != "")
                    {
                        Select = ColorRepeat;
                        break;
                    }
                }
            }

            if (Select.Equals("Red", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            if (Select.Equals("Cyan", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            if (Select.Equals("Yellow", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            if (Select.Equals("Magenta", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            if (Select.Equals("Green", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            if (Select.Equals("Blue", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }

            Console.WriteLine($"[Bot: {nameBot}] - Chat background color changed");
            #endregion
        }
        public void BotChatClear()
        {
            #region chatclear

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                     MESSAGE");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" ");

            Console.ResetColor();

            Console.WriteLine($"[Bot: {nameBot}] - Chat clear!");

            #endregion
        }
        public void BotErrorCheck()
        {
            #region errorcheck

            using (var err = new StreamReader("logError.txt", Encoding.UTF8))
            {
                Console.WriteLine("If nothing happened, the error log is empty");

                var read = err.ReadToEnd();
                Console.WriteLine(read);
            }

            Console.WriteLine($"[Bot: {nameBot}] - I got a list of errors all the time");

            #endregion
        }
    }
}
