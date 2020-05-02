using System;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace ChatRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            #region info

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                    CHAT ROOM");
            Console.WriteLine("--------------------------------------------------");

            Console.ResetColor();
            Console.WriteLine(" ");

            #endregion

            #region server

            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(int.MaxValue);

            while(true)
            {
                var listeren = tcpSocket.Accept();
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listeren.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listeren.Available > 0);

                Console.WriteLine(data);

                listeren.Send(Encoding.UTF8.GetBytes($"Messages sent.{Environment.NewLine}"));

                listeren.Shutdown(SocketShutdown.Both);
                listeren.Close();
            }
            #endregion
        }
    }
}
