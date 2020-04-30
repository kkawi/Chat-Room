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
            #region botcmd

            Console.WriteLine("there is a bot in the chat SC9, its commands @bot / cmd");

            #endregion

            #region name

            User user = new User();

            string Log = "Log: ";

            Console.WriteLine("Enter your nickname for chat:");
            user.UserName = Console.ReadLine();

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
                    Console.WriteLine($"[Bot: {bot.nameBot}] - my command: {Environment.NewLine}@bot /name - show your chat nickname.{Environment.NewLine}@bot /loglist - show error log.{Environment.NewLine}@bot /math (example) - math calculation.{Environment.NewLine}@bot /reset - reset color text/background.");
                    Console.WriteLine($"@bot /clear - clear chat.{Environment.NewLine}@bot /tcolor - change chat text color.{Environment.NewLine}@bot /bcolor - change chat background color.{Environment.NewLine}@bot /rnd - random number in the specified range.");
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
                    Console.Clear();
                    Console.WriteLine($"[Bot: {bot.nameBot}] - Chat clear!");
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
    }
}
