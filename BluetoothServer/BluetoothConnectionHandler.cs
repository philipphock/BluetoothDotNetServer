using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.IO;
namespace BluetoothServer
{
    class BluetoothConnectionHandler
    {

        private bool read = true;
        private System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        private readonly BluetoothClient btClient;
        public BluetoothConnectionHandler(BluetoothClient client){
            Console.WriteLine("init handler");
            this.btClient = client;
        }


        public void Handle()
        {
            byte[] buffer = new byte[64];
			int received = 0;

            Console.WriteLine("handler handles");
            Stream peerStream = btClient.GetStream();
            while (read)
            {
                received = peerStream.Read(buffer,0,64);
                if (received <= 0)
                {
                    break;
                }
                String s = encoding.GetString(buffer, 0, received);
                Console.WriteLine("Recv: " + s);
                Send(s);
            }
            

            Console.WriteLine("handler finish handling");
        }

        public void Send(string data)
        {
            Stream s = this.btClient.GetStream();
            byte[] toSend = encoding.GetBytes(data);
            s.Write(toSend,0,toSend.Length);

        }

    }
}
