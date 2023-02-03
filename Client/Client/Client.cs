using System;

using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Client
    {
        public static TcpClient client = new TcpClient();
        public const int BufferSize = 1024;

        // public static ile  mainden Client.ClientConnect() diyerek erişebilmek.
        public static void ClientConnect()
        {
            try
            {
                client.BeginConnect(ServerConnSetting._host, ServerConnSetting._port, new AsyncCallback(TCPConnectCallback), client); //asenkron olarak bağlanır.
                Console.WriteLine("Bağlanılıyor");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void TCPConnectCallback(IAsyncResult result)
        {
            try
            {
                client.EndConnect(result);
                if(client.Connected)
                {
                    Console.WriteLine("Bağlanıldı...");
                }

                #region Send Message to Server

                string message = "Hello";

                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
                // Get a client stream for reading and writing. 
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length); //(**This is to send data using the byte method**) 

                #endregion

                #region Receive Response from Server

                Byte[] receivedData = new Byte[1024];

                String responseData = String.Empty;
                NetworkStream ns = client.GetStream();
         
                Int32 bytes = ns.Read(receivedData, 0, receivedData.Length);
                responseData = System.Text.Encoding.ASCII.GetString(receivedData, 0, bytes);
                Console.WriteLine("Gelen Mesaj: {0}", responseData);

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    } 
}
