using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Net.Sockets;
using System.IO;
namespace BluetoothServer
{
    
    
    class BluetoothServer
    {
        private Guid MyService = new Guid("{94d6b2c0-8034-11e3-baa7-0800200c9a66}");
        private bool listening = true;

        public BluetoothServer(){
            Console.WriteLine("Starting server");
            BluetoothRadio br = BluetoothRadio.PrimaryRadio;
            if (br == null)
            {
                Console.WriteLine("no bluetooth device found");
                return;
            }
            else if (br.Mode != InTheHand.Net.Bluetooth.RadioMode.Discoverable)
            {
                Console.WriteLine("device not discoverable..");
                br.Mode = RadioMode.Discoverable;
                Console.WriteLine("device now discoverable");
                
            }


            StartListening();
           
           
        }

     

        public void StartListening()
        {
            
            BluetoothListener btl = new BluetoothListener(MyService);
            btl.Start();
            listening = true;
            while (listening)
            {
                Console.WriteLine("wait for device");
                BluetoothClient conn = btl.AcceptBluetoothClient();
                Console.WriteLine("device accepted");
                DisplatchConnection(conn);

            }

        }

        private void DisplatchConnection(BluetoothClient client)
        {
            BluetoothConnectionHandler handler = new BluetoothConnectionHandler(client);
            handler.Handle();
        }


    }
    
        
}

  
