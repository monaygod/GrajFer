using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
   class Program
   {
      private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      static void Main(string[] args)
      {
         Console.Title = "Client";
         Console.WriteLine("Enter server ip:");
         string ip = Console.ReadLine();
         IPAddress _ip = System.Net.IPAddress.Parse(ip);
         IPAddress test = IPAddress.Loopback;
         Connection(_ip);
         MsgLoop();
         Console.ReadLine();
      }

      private static void Connection(IPAddress _ip)
      {
         while (!_clientSocket.Connected)
         {
            try
            {
               _clientSocket.Connect(_ip, 1234);
            }
            catch (SocketException)
            {
               Console.WriteLine("...trying to connect");
            }
         }
         Console.Clear();
         Console.WriteLine(@"  
            ===================================================  
                   Connected 
            ===================================================");
      }

      private static void MsgLoop()
      {
         while (true)
         {
            Console.WriteLine("enter text:");
            string msg = Console.ReadLine();
            byte[] buffer = Encoding.ASCII.GetBytes(msg);
            _clientSocket.Send(buffer);

            byte[] recBuffer = new byte[1024];
            int rec = _clientSocket.Receive(recBuffer);
            byte[] data = new byte[rec];
            Array.Copy(recBuffer, data, rec);
            Console.WriteLine("Server response: " + Encoding.ASCII.GetString(data));
         }
      }
   }
}

