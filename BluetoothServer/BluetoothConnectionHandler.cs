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
    abstract class BluetoothConnectionHandler
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
                try
                {
                    received = peerStream.Read(buffer, 0, 64);
                    if (received <= 0)
                    {
                        break;
                    }
                    String s = encoding.GetString(buffer, 0, received);
                    Recv(s);
                }
                catch (Exception)
                {
                    Cancel();
                }
            }
            

            Console.WriteLine("handler finish handling");
        }

        public void Send(string data)
        {
            Stream s = this.btClient.GetStream();
            byte[] toSend = encoding.GetBytes(data);
            try
            {
                s.Write(toSend, 0, toSend.Length);

            }
            catch (IOException)
            {
                Cancel();
            }

        }
        public void Cancel()
        {
            read = false;
            try
            {

                btClient.GetStream().Close();
                btClient.Close();
            }
            catch (Exception) { }
        }
        protected abstract void Recv(string s);

    }
}
