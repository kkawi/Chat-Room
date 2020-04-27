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

            Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"there is a {bot.nameBot} bot in the chat, use @bot / cmd to find out his commands");

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("Enter your nickname for chat:");
            user.userName = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(user.userName))
            {
                var ErorNullName = "Name cannot be empty!";

                using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                {
                    er.WriteLine(Log + ErorNullName);
                }

                throw new ArgumentNullException("Name cannot be empty!");
            }

            string _Message = " Message: ";
            string _Name = "Name: ";
            string _Do = ".";
            #endregion

            #region message
            while (true)
            {
                const string ip = "127.0.0.1";
                const int host = 8080;

                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), host);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Enter messages:");
                string Message = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Message))
                {
                    var ErrorNullMessage = "Messages cannot be empty!";

                    using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                    {
                        er.WriteLine(Log + ErrorNullMessage);
                    }

                    throw new ArgumentNullException("Messages cannot be empty");
                }

                #region bot

                if (Message.Equals("@bot /cmd", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[{bot.nameBot}] my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.{Environment.NewLine}@bot /math (example) - math calculation.");
                }

                if (Message.Equals("@bot /name", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine($"[{bot.nameBot}] - your chat nickname: {user.userName}");
                }
                if (Message.Equals("@bot /math", StringComparison.CurrentCultureIgnoreCase))
                {

                }
                if(Message.Equals($"@bot /math", StringComparison.CurrentCultureIgnoreCase))
                {
                    Math();
                }
                if (Message.Equals("@bot /loglist", StringComparison.CurrentCultureIgnoreCase))
                {
                    using(var err = new StreamReader("logError.txt", Encoding.UTF8))
                    {
                        Console.WriteLine("If nothing happened, the error log is empty");

                        var read = err.ReadToEnd();
                        Console.WriteLine(read);
                    }
                }
                #endregion

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
        public static void Math()
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
    }
}
