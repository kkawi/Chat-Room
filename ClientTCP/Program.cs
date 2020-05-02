using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace ClientTCP
{
    class Program
    {
        static void Main(string[] args)
        {
            #region info

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                     MESSAGE");
            Console.WriteLine("--------------------------------------------------");

            Console.ResetColor();
            Console.WriteLine(" ");

            Console.WriteLine("there is a bot in the chat SC9, its commands @bot / cmd");

            #endregion

            #region name

            User user = new User();

            Console.WriteLine("Enter your name:");
            user.userName = Console.ReadLine();


            #region set

            if (string.IsNullOrWhiteSpace(user.userName))
            {
                for (var i = 0; i == 0;)
                {
                    Console.WriteLine($"The name cannot be empty, try entering the name again:{Environment.NewLine}");

                    user.userName = Console.ReadLine();

                    if (user.userName != "")
                    {
                        i = 1;

                        break;
                    }
                }
            }

            string fileText = File.ReadAllText("userActive.txt");

            if (fileText.Contains(user.userName))
            {
                for (var i = 0; i == 0;)
                {
                    Console.WriteLine($"the name is already in use, try entering again:{Environment.NewLine}");

                    user.userName = Console.ReadLine();

                    if (fileText.Contains(user.userName) == false)
                    {
                        i = 1;

                        break;
                    }
                }
            }

            using (var sw = new StreamWriter("userActive.txt", false, Encoding.UTF8))
            {
                sw.WriteLine(user.userName);
            }

            #endregion

            Console.WriteLine("You entered the chat");

            #endregion

            #region markup

            string Log = $"[{DateTime.Now.ToString()}] - Log: ";

            string _Message = " Message: ";
            string _Name = "Name: ";
            string _Dot = ".";

            #endregion

            #region message/bot

            try
            {
                while (true)
                {

                    const string ip = "127.0.0.1";
                    const int port = 8080;

                    var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    Console.WriteLine("Enter messages:");
                    string Message = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(Message))
                    {
                        var messageError = "Messages cannot be empty!";

                        using (var er = new StreamWriter("logError.txt", true, Encoding.UTF8))
                        {
                            er.WriteLine(Log + messageError);
                        }

                        throw new ArgumentNullException("Messages cannot be empty");
                    }

                    // bot.

                    Bot bot = new Bot();

                    if (Message.Equals("@bot /cmd", StringComparison.CurrentCultureIgnoreCase))
                    {
                        // command list bot.

                        Console.WriteLine($"[Bot: {bot.nameBot}] - my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.");
                        Console.WriteLine($"@bot /math (example) - math calculation.{Environment.NewLine}@bot /reset - reset color text/background.");
                        Console.WriteLine($"@bot /clear - clear chat.{Environment.NewLine}@bot /tcolor - change chat text color.");
                        Console.WriteLine($"@bot /bcolor - change chat background color.{Environment.NewLine}@bot /rnd - random number in the specified range.");
                        Console.WriteLine($"@bot /setname - name changes.");
                    }

                    if (Message.Equals("@bot /name", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine($"[Bot: {bot.nameBot}] - your chat nickname: {user.userName}");
                    }
                    if (Message.Equals("@bot /rnd", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bot.BotRandom();
                        Console.WriteLine($"[Bot: {bot.nameBot}] - Random number regeneration completed");
                    }
                    if (Message.Equals("@bot /clear", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bot.BotChatClear();
                    }
                    if (Message.Equals($"@bot /math", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bot.BotMath();
                        Console.WriteLine($"[Bot: {bot.nameBot}] - Bot completed the robot with computations");
                    }
                    if (Message.Equals($"@bot /tcolor", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bot.BotTextColor();
                        Console.WriteLine($"[Bot: {bot.nameBot}] - Chat text color changed");
                    }
                    if (Message.Equals($"@bot /reset", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.ResetColor();
                        Console.WriteLine($"[Bot: {bot.nameBot}] - Background / chat color has been changed to normal");
                    }
                    if (Message.Equals($"@bot /bcolor", StringComparison.CurrentCultureIgnoreCase))
                    {
                        bot.BotBackgroundColor();
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

                    tcpSocket.Connect(tcpEndPoint);

                    if (Message.Equals("@bot /setname", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter your new name:");

                        user.userNameSet = Console.ReadLine();

                        SetName();

                        void SetName()
                        {
                            if (fileText.Contains(user.userNameSet))
                            {
                                for (var i = 0; i == 0;)
                                {
                                    Console.WriteLine($"the name is already in use, try entering again:{Environment.NewLine}");

                                    user.userNameSet = Console.ReadLine();

                                    if (fileText.Contains(user.userNameSet) == false)
                                    {
                                        i = 1;

                                        break;
                                    }
                                }
                            }

                            var _setname = Encoding.UTF8.GetBytes($"{user.userName} изменил имя на: {user.userNameSet}{Environment.NewLine}");

                            tcpSocket.Send(_setname);

                            user.userName = user.userNameSet;
                        }
                    }

                    var _nick = Encoding.UTF8.GetBytes(user.userName);
                    var _data = Encoding.UTF8.GetBytes(Message);
                    var _message = Encoding.UTF8.GetBytes(_Message);
                    var _name = Encoding.UTF8.GetBytes(_Name);
                    var _dot = Encoding.UTF8.GetBytes(_Dot);

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
            }
            catch(SocketException)
            {
                Console.WriteLine("Socket exception, server on device is not open.");

                var socketException = $"{Log}Socket exception, server on device is not open.";

                using(var sw = new StreamWriter("logError.txt", true, Encoding.UTF8))
                {
                    sw.WriteLine(socketException);
                }
            }

            #endregion
        }
    }
}
