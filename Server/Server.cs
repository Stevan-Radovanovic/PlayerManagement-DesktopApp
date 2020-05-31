using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public class Server
    {
        public Socket listeningSocket;
        public bool kraj = false;

        public bool StartServer()
        {
            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listeningSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9090));
                listeningSocket.Listen(5);

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public void BeOnTheLookout()
        {
            while (!kraj)
            {
                try
                {
                    Socket clientSocket = listeningSocket.Accept();
                    Interaction interaction = new Interaction(clientSocket);
                    Thread clientThread = new Thread(interaction.Request);
                    clientThread.Start();
                }
                catch
                {
                    kraj = true;
                }
            }
        }
    }
}
