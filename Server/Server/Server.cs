using System;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Server:IDisposable
    {
        // burada server işlemleri yaptığım için TcpListener kullandım
        public int _port { get; private set; }

        public static TcpListener serverListener;

        public Server(int port)
        {
            _port = port;

            serverListener = new TcpListener(IPAddress.Any, _port);

            Console.WriteLine($"Server Kuruldu");
        }

        public void StartServer()
        {
            try
            {
                serverListener.Start();
                Console.WriteLine($"Server kuruldu, dinlenen port: {_port}");
                serverListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), serverListener); 
                Console.WriteLine("Client bekleniyor");
                //NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

              
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
                TcpClient client = serverListener.EndAcceptTcpClient(result);
                Console.WriteLine($"Bir client içeri girdi {client.Client.RemoteEndPoint}"); // client'a özel ip ve kimlik numarasına içiren endpoit
                serverListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), serverListener);

                #region Receive Message from Client

                // Buffer to store the response bytes.
                Byte[] data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;
                NetworkStream ns = client.GetStream();
                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = ns.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Gelen Mesaj: {0}", responseData);

                #endregion

                #region Send Response to Client 
                DateTime now = DateTime.Now;
                string responseMessage = $"{responseData} - {now.ToString()}";
                NetworkStream stream = client.GetStream();

                Byte[] response = System.Text.Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(response, 0, response.Length);


                #endregion
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
