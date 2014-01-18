using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BluetoothServer
{

    class Command
    {
        private readonly Dictionary<string, Func<string,string>> commandsDict =  new Dictionary<string, Func<string,string>>();
        public static readonly string COMMAND_NOT_FOUND = "err_command_not_found";


        private bool _restartHandler = false;

        public bool RestartConnectionHandler
        {
            get {
                bool ret = _restartHandler;
                _restartHandler = false;
                return ret; 
            }
            
        }

        public Command()
        {
            commandsDict.Add("standby", standby);   

        }


        public string execute(string s)
        {
            Console.WriteLine("Command recv " + s);

            if (commandsDict.ContainsKey(s))
            {
                Console.WriteLine("Command correct: " + s);

                return commandsDict[s]("");
            }
            Console.WriteLine("Command not found: " + s);

            throw new CommandNotFoundException();

            
        }

        public string standby(string s){
            Console.WriteLine("Performing standby: " + s);
            Application.SetSuspendState(PowerState.Suspend, true, true);
            _restartHandler = true;
            return "ok_standby";
        }
    }



}
