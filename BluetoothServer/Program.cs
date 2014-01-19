using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BluetoothServer
{
    class Program
    {

        public static BluetoothServer server;  
        static void Main(string[] args)
        {
            server = new BluetoothServer();
            Thread t = new Thread(input);
            t.Start();
            while (true)
            {
                string line = Console.ReadLine();
                server.Send(line);
            }

        }

        public static void input()
        {
            server.StartListening();

           
            
        }
    }
}
