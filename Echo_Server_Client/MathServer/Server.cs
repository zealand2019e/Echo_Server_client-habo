using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathServer
{
    class Server
    {
        public void Start ()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 7777;
                IPAddress localAddr = IPAddress.Loopback;

                int clientNumber = 0;

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Task.Run(() => HandleStream(client,ref clientNumber));
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        public void HandleStream(TcpClient client, ref int clientNumber)
        {
            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;
            clientNumber++;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0} from client {1}", data, clientNumber);

                try
                {
                    // Process the data sent by the client.
                    data = "result " + DoTheMath(data);
                }
                catch (ArgumentException e)
                {

                    data = e.Message;
                }

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                Thread.Sleep(1000);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }

            // Shutdown and end connection
            client.Close();
            clientNumber--;
        }

        public float DoTheMath(string input)
        {
            string[] tokens = input.Split(' ');
            switch (tokens[0])
            {
                case "ADD":
                    return float.Parse(tokens[1]) + float.Parse(tokens[2]);
                case "SUB":
                    return float.Parse(tokens[1]) - float.Parse(tokens[2]);
                case "MUL":
                    return float.Parse(tokens[1]) * float.Parse(tokens[2]);
                case "DIV":
                    return float.Parse(tokens[1]) / float.Parse(tokens[2]);
                default:
                    throw new ArgumentException("Invalid operation");
                    break;
            }
        }
       
    }
}
