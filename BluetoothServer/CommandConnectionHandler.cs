using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothServer
{

    
    class CommandConnectionHandler : BluetoothConnectionHandler
    {
        private readonly Command commands;

        public CommandConnectionHandler(BluetoothClient client): base(client)
        {
            
            commands = new Command();
        }

        protected override void Recv(String s){
            Console.WriteLine("Recv: " + s);

            try
            {

                string cmd_result = commands.execute(s);
                Send(cmd_result);
                if (commands.RestartConnectionHandler)
                {
                    this.Cancel();
                }
            }
            catch (CommandNotFoundException )
            {
                Send(Command.COMMAND_NOT_FOUND);
            }

            
        }
    }
}
