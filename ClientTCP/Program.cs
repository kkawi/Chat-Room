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
            Console.ForegroundColor = ConsoleColor.Yellow;

            User user = new User();

            Console.WriteLine("Enter your nickname for chat:");
            user.userName = Console.ReadLine();

            if(string.IsNullOrWhiteSpace(user.userName))
            {
                throw new ArgumentNullException("Name cannot be empty!");
            }

            string _Message = " Message: ";
            string _Name = "Name: ";
            string _Do = ".";

            while(true)
            {
                const string ip = "127.0.0.1";
                const int host = 8080;

                var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), host);
                var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Console.WriteLine("Enter messages:");
                string Message = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(Message))
                {
                    throw new ArgumentNullException("Messages cannot be empty!");
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
    }
}
