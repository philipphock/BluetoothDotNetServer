using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluetoothServer
{
    class MusicPlayerControl
    {

        private static volatile MusicPlayerControl instance;
        private static object syncRoot = new Object();



        private MusicPlayerControl()
        {
        }

        public static MusicPlayerControl Instance
        {
            get 
            {
                if (instance == null) 
                {
                lock (syncRoot) 
                {
                    if (instance == null)
                        instance = new MusicPlayerControl();
                }
                }

                return instance;
            }
        }


        public void PlayPause()
        {
            Execute("/playpause");
        }

        public void Next()
        {
            Execute("/next");
        }


        public void Prev()
        {
            Execute("/prev");
        }

        private void Execute(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Program Files (x86)\foobar2000\foobar2000.exe";
            startInfo.Arguments = arg;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            Process.Start(startInfo);
        }


    }



    
}
