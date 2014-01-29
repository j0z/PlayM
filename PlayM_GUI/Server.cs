using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace PlayM_GUI
{
    class Server
    {
        private TcpListener tcpListener;
        private Thread listenThread;

        private InputSynth inputSynth = new InputSynth();

        public Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Any, 3000);
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();

            while(true)
            {
                TcpClient client = this.tcpListener.AcceptTcpClient();

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            Debug.WriteLine("Client Connected. \n");

            byte[] message = new byte[4096];
            int bytesRead;

            while(true)
            {
                bytesRead = 0;

                try
                {
                    bytesRead = clientStream.Read(message, 0, 512);

                }

                //socket error
                catch
                {
                    Console.WriteLine("ERROR: Connection Error. \n");
                    break;
                }

                //client disconnected
                if(bytesRead == 0)
                {
                    Console.WriteLine("ERROR: Client Disconnected. \n");
                    break;
                }

                ASCIIEncoding encoder = new ASCIIEncoding();
                string key = encoder.GetString(message, 0, bytesRead);
                Debug.WriteLine(key);

                string[] dataPackets = key.Split('\n');
                foreach (string s in dataPackets)
                {
                    string[] data = s.Split(';');

                    if (data[0] == "KB")
                    {
                        Debug.WriteLine("Keyboard: ");
                        inputSynth.keyPress(data[1], Convert.ToInt32(data[2]));
                    }
                    if (data[0] == "M")
                    {
                        Debug.WriteLine("Mouse");
                        inputSynth.mouseMove(data[1]);
                    }
                }

            }

            tcpClient.Close();
        }
    }
}

