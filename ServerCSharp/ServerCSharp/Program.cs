using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ServerCSharp
{
   class Program
   {
      private const int SERVERPORT = 1234;
      private static Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      private static List<Socket> clientSockets = new List<Socket>();
      private const int bufferSize = 1024;
      private static byte[] _buffer = new byte[bufferSize];

      static void Main(string[] args)
      {
         Console.Title = "Server";
         ServerSetup();
         Console.ReadLine();
         CloseSockets();
      }

      private static void ServerSetup()
      {
         //IPEndPoint ep = new IPEndPoint(IPAddress.Any, 1234);
         IPEndPoint ep = new IPEndPoint(IPAddress.Parse(GetServerMachineIP()), SERVERPORT);

         listener.Bind(ep);
         listener.Listen(5); //backlog
         listener.BeginAccept(AcceptCallback, null);
         Console.WriteLine(@"  
            ===================================================  
                   Listening at: {0}:{1}  
            ===================================================",
            ep.Address, ep.Port);
      }

      private static void AcceptCallback(IAsyncResult ar)
      {
         Socket socket;
         try
         {
            socket = listener.EndAccept(ar);
         }
         catch (ObjectDisposedException)
         {
            return;
         }
         clientSockets.Add(socket);
         Console.WriteLine(@"       Client Connected");
         socket.BeginReceive(_buffer, 0, bufferSize, SocketFlags.None, RecieveCallback, socket);
         listener.BeginAccept(AcceptCallback, null);
      }

      private static void RecieveCallback(IAsyncResult ar)
      {
         Socket socket = (Socket)ar.AsyncState;
         int receivedBytesNo;
         try
         {
            receivedBytesNo = socket.EndReceive(ar);
         }
         catch (SocketException)
         {
            Console.WriteLine(@"       Client forcefully disconnected");
            socket.Close();
            clientSockets.Remove(socket);
            return;
         }

         //Buffer recieved from the clients
         byte[] tempBuf = new byte[receivedBytesNo];
         Array.Copy(_buffer, tempBuf, receivedBytesNo);
         
         string text = Encoding.ASCII.GetString(tempBuf);
         Console.WriteLine("Received: " + text);

         //Server response logic to implement
         string response = text;
         socket.Send(Encoding.ASCII.GetBytes(response));

         socket.BeginReceive(_buffer, 0, bufferSize, SocketFlags.None, RecieveCallback, socket);
      }


      private static void SendCallback(IAsyncResult ar)
      {
         Socket socket = (Socket)ar.AsyncState;
         socket.EndSend(ar);
      }

      //
      private static string GetServerMachineIP()
      {
         string strHostName = "";
         strHostName = Dns.GetHostName();
         IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
         IPAddress[] addr = ipEntry.AddressList;
         return addr[addr.Length - 1].ToString();
      }

      private static void CloseSockets()
      {
         foreach (Socket socket in clientSockets)
         {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
         }
         listener.Close();
      }

   }
}

