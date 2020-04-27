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
            #region name
            string Log = "Log: ";

            User user = new User();
            Bot bot = new Bot();
            bot.nameBot = "Bot kkawi";
            
            Console.WriteLine($"there is a {bot.nameBot} bot in the chat, use @bot / cmd to find out his commands");

            Console.WriteLine("Enter your nickname for chat:");
            user.userName = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(user.userName))
            {
                var ErorNullName = "Name cannot be empty!";

                using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                {
                    er.WriteLine(Log + ErorNullName);
                }

                for(var i = 0; i == 0;)
                {
                    Console.WriteLine("The name cannot be empty, try entering the name again");

                    string NameRepeat = Console.ReadLine();
                    user.userName = NameRepeat;

                    if(NameRepeat != "")
                    {
                        user.userName = NameRepeat;
                        break;
                    }
                }
            }

            Console.WriteLine("Are you chatting!");

            string _Message = " Message: ";
            string _Name = "Name: ";
            string _Do = ".";
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

                if (Message.Equals("@bot /cmd", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[Bot: {bot.nameBot}] - my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.{Environment.NewLine}@bot /math (example) - math calculation.{Environment.NewLine}@bot /rnd - random number in the specified range.{Environment.NewLine}@bot /clear - clear chat.{Environment.NewLine}@bot /color - change chat text color");
                }

                if (Message.Equals("@bot /name", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[Bot: {bot.nameBot}] - your chat nickname: {user.userName}");
                }
                if (Message.Equals("@bot /rnd", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotRandom();
                }
                if (Message.Equals("@bot /clear", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.Clear();
                    Console.WriteLine("Chat clear!");
                }
                if (Message.Equals($"@bot /math", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotMath();
                }
                if (Message.Equals($"@bot /color", StringComparison.CurrentCultureIgnoreCase))
                {
                    BotColor();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Chat color changed");
                }
                if (Message.Equals("@bot /loglist", StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var err = new StreamReader("logError.txt", Encoding.UTF8))
                    {
                        Console.WriteLine("If nothing happened, the error log is empty");

                        var read = err.ReadToEnd();
                        Console.WriteLine(read);
                    }
                }

                var data = Encoding.UTF8.GetBytes(Message);
                var nick = Encoding.UTF8.GetBytes(user.userName);
                var _message = Encoding.UTF8.GetBytes(_Message);
                var _name = Encoding.UTF8.GetBytes(_Name);
                var _do = Encoding.UTF8.GetBytes(_Do);

                tcpSocket.Connect(tcpEndPoint);

                tcpSocket.Send(_name);
                tcpSocket.Send(nick);
                tcpSocket.Send(_do);
                tcpSocket.Send(_message);
                tcpSocket.Send(data);

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
        }
        #endregion
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
        public static void BotColor()
        {
            #region color
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("             Available colors:");
            Console.WriteLine("--------------------------------------------");

            Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Red"); 
            Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine("Cyan"); 
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Yellow");
            Console.ForegroundColor = ConsoleColor.Magenta; Console.WriteLine("Magenta"); 
            Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("Green"); 
            Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine("Blue");

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine("Enter the names of one of the colors you want to choose");
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
    }
}
