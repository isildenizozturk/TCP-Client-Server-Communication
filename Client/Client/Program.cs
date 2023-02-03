using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConnSetting.SetServer("127.0.0.1", 8585);
            Client.ClientConnect();
            Console.ReadLine();
        }
    }
}
