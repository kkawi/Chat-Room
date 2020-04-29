using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region botcmd

            Console.WriteLine("there is a bot in the chat SC9, its commands @bot / cmd");

            #endregion

            #region name

            User user = new User();

            string Log = "Log: ";

            Console.WriteLine("Enter your nickname for chat:");
            user.userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(user.userName))
            {
                var ErorNullName = "Name cannot be empty!";

                using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                {
                    er.WriteLine(Log + ErorNullName);
                }

                for (var i = 0; i == 0;)
                {
                    Console.WriteLine("The name cannot be empty, try entering the name again");

                    string NameRepeat = Console.ReadLine();
                    user.userName = NameRepeat;

                    if (NameRepeat != "")
                    {
                        user.userName = NameRepeat;

                        break;
                    }
                }
            }

            Console.WriteLine("You entered the chat");

            #endregion

            #region markup

            string _Message = " Message: ";
            string _Name = "Name: ";
            string _Dot = ".";

            #endregion

            #region message/bot

            while (true)
            {

                const string ip = "127.0.0.1";
                const int host = 8080;

                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), host);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Enter messages:");
                string Message = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Message))
                {
                    var ErrorNullMessage = "Messages cannot be empty!";

                    using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                    {
                        er.WriteLine(Log + ErrorNullMessage);
                    }

                    throw new ArgumentNullException("Messages cannot be empty");
                }

                // bot

                Bot bot = new Bot();

                if (Message.Equals("@bot /cmd", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[Bot: {bot.nameBot}] - my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.{Environment.NewLine}@bot /math (example) - math calculation.");
                    Console.WriteLine($"@bot /clear - clear chat.{Environment.NewLine}@bot /tcolor - change chat text color.{Environment.NewLine}@bot /bcolor - change chat background color.{Environment.NewLine}@bot /rnd - random number in the specified range.{Environment.NewLine}@bot /reset - reset color text/background");
                }

                if (Message.Equals("@bot /name", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[Bot: {bot.nameBot}] - your chat nickname: {user.userName}");
                }
                if (Message.Equals("@bot /rnd", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotRandom();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Random number regeneration completed");
                }
                if (Message.Equals("@bot /clear", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Chat clear!");
                }
                if (Message.Equals($"@bot /math", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotMath();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Bot completed the robot with computations");
                }
                if (Message.Equals($"@bot /tcolor", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotTextColor();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Chat text color changed");
                }
                if (Message.Equals($"@bot /reset", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.ResetColor();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Background / chat color has been changed to normal");
                }
                if (Message.Equals($"@bot /bcolor", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotBackgroundColor();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Chat background color changed");
                }
                if (Message.Equals("@bot /loglist", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var err = new StreamReader("logError.txt", Encoding.UTF8))
                    {
                        Console.WriteLine("If nothing happened, the error log is empty");

                        var read = err.ReadToEnd();
                        Console.WriteLine(read);
                    }

                    Console.WriteLine($"[Bot: {bot.nameBot}] - I got a list of errors all the time");
                }

                var _nick = Encoding.UTF8.GetBytes(user.userName);
                var _data = Encoding.UTF8.GetBytes(Message);
                var _message = Encoding.UTF8.GetBytes(_Message);
                var _name = Encoding.UTF8.GetBytes(_Name);
                var _dot = Encoding.UTF8.GetBytes(_Dot);

                tcpSocket.Connect(tcpEndPoint);

                tcpSocket.Send(_name);
                tcpSocket.Send(_nick);
                tcpSocket.Send(_dot);
                tcpSocket.Send(_message);
                tcpSocket.Send(_data);

                var buffer = new byte[256];
                var size = 0;
                var answer = new StringBuilder();

                do
                {
                    size = tcpSocket.Receive(buffer);
                    answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (tcpSocket.Available > 0);

                Console.WriteLine(answer.ToString());

                tcpSocket.Shutdown(SocketShutdown.Both);
                tcpSocket.Close();
            }

            #endregion
        }

        public static void BotMath()
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
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }
        public static void BotRandom()
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
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }
        public static void BotTextColor()
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
            #endregion
        }
        public static void BotBackgroundColor()
        {
            #region backgroundcolor

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("         Available colors for changing chat background:");
            Console.WriteLine("----------------------------------------------------------------");

            if(Console.BackgroundColor != ConsoleColor.White)
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

            #endregion
        }
    }
}
