using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace PlayM_Controller_Legacy
{
    class Client
    {
        TcpClient client = new TcpClient();
        IPEndPoint server = new IPEndPoint(IPAddress.Parse("10.0.0.12"), 3000); //Change this to the IP of the server

        /// <summary>
        /// start the client, continue trying to connect until server is reached.
        /// </summary>
        public void startClient()
        {
            try
            {
                client.Connect(server);
                Console.WriteLine("Server Connected! \n");
                ServicePointManager.UseNagleAlgorithm = false; //we need to get the packets out ASAP!
            }
            catch
            {
                Console.WriteLine("Server Connection Failed. Trying again in 5sec...");
                System.Threading.Thread.Sleep(5000);                                        //Try again
                startClient();
            }
        }

        /// <summary>
        /// Send the message to the server
        /// </summary>
        /// <param name="type">KB or M</param>
        /// <param name="message">Single character or x,y</param>
        /// <param name="hold">0=default, 1=press, 2=release</param>
        public void send(string type, string message, int hold = 0)
        {
            try
            {
                NetworkStream clientStream = client.GetStream();

                ASCIIEncoding encoder = new ASCIIEncoding();

                byte[] buffer = encoder.GetBytes(type + ';' + message + ';' + hold + "\n");

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
            catch
            {
                Console.WriteLine("Connection Lost");
                startClient();
            }
        }
    }
}
