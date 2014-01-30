using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Networking.Sockets;
using Windows.Networking;
using System.Threading;
using Windows.Storage.Streams;


namespace Pupi
{
    class Client
    {
        StreamSocket client = new StreamSocket();
        EndpointPair server = new EndpointPair(null, "", new HostName("10.0.0.2"), "3000"); //Change this to the IP of the server

        /// <summary>
        /// start the client, continue trying to connect until server is reached.
        /// </summary>
        public async void startClient()
        {
            try
            {
                await client.ConnectAsync(server);
                //Console.WriteLine("Server Connected! \n");
                //ServicePointManager.UseNagleAlgorithm = false; //we need to get the packets out ASAP!
            }
            catch
            {
                //Console.WriteLine("Server Connection Failed. Trying again in 5sec...");

                new System.Threading.ManualResetEvent(false).WaitOne(5000);                                     //Try again
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
                //NetworkStream clientStream = client.GetStream();
                DataWriter writer = new DataWriter(client.OutputStream);

                
                //ASCIIEncoding encoder = new ASCIIEncoding();

                string buffer = type + ';' + message + ';' + hold + "\n";
                writer.WriteUInt32(writer.MeasureString(buffer));
                writer.WriteString(buffer);
                /*
                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
                 * */
                writer.WriteString(buffer);
            }
            catch
            {
                //Console.WriteLine("Connection Lost");
                startClient();
            }
        }
    }
}
