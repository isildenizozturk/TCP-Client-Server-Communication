using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Server server = new Server(8585))
            {
                server.StartServer();
            }

            Console.ReadLine();
        }
    }
}
