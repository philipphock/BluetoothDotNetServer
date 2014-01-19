using System;
using System.Collections.Generic;
using System.Globalization;
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
            commandsDict.Add("standby", Standby);
            commandsDict.Add("volume", Volume);
            commandsDict.Add("musicplayer", Musicplayer);
            commandsDict.Add("togglemute", ToggleMute);
            commandsDict.Add("request_status", Request_status);
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

        }


        public string Execute(string s,string param)
        {
            Console.WriteLine("Command recv " + s);

            if (commandsDict.ContainsKey(s))
            {
                Console.WriteLine("Command correct: " + s);

                return commandsDict[s](param);
            }
            Console.WriteLine("Command not found: " + s);

            throw new CommandNotFoundException();

            
        }

        public string Standby(string s){
            Console.WriteLine("Performing standby: " + s);
            Application.SetSuspendState(PowerState.Suspend, true, true);
            _restartHandler = true;
            return "ok_standby";
        }

        public string Volume(string percent)
        {


            float v = float.Parse(percent,CultureInfo.InvariantCulture.NumberFormat);
            AudioControl.Instance.SetMasterVolume(v);
            return "ok_volume: " + AudioControl.Instance.GetMasterVolume();
        }


        public string ToggleMute(String s)
        {
            AudioControl.Instance.ToggleMute();
            return "ok_mute";
        }

        public string Musicplayer(string command)
        {
            switch(command){
                case "playpause":
                    MusicPlayerControl.Instance.PlayPause();
                    break;

                case "next":
                    MusicPlayerControl.Instance.Next();
                    break;

                case "prev":
                    MusicPlayerControl.Instance.Prev();
                    break;
            }
            
            return "ok_musicplayer";
        }
        public string Request_status(string s)
        {
            float mastervolf = 64.0f/(64-AudioControl.Instance.GetMasterVolume());
            string mastervol = mastervolf+"";
            return @"{'vol':"  + mastervol.Replace(',','.') + "}";
        }

    }

  
}
