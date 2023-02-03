using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ServerConnSetting
    {
        public static string _host { get; private set; }
        public static int _port { get; private set; }

        public static void SetServer(string host, int port)
        {
            _host = host;
            _port = port;
        }

    }
}
