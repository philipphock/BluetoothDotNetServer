using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
           
            
            
            if(!SendHTTP("http://192.168.1.2:9999/ajquery/?cmd=PlayOrPause&param3=NoResponse")){
                Execute("/play");
            }
        }

        public void Next()
        {
            SendHTTP("http://192.168.1.2:9999/ajquery/?cmd=StartNext&param3=NoResponse");
        }
        


        public void Prev()
        {
            SendHTTP("http://192.168.1.2:9999/ajquery/?cmd=StartPrevious&param3=NoResponse");
        }

        private void Execute(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Program Files (x86)\foobar2000\foobar2000.exe";
            startInfo.Arguments = arg;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            Process.Start(startInfo);
            Console.WriteLine("executed foobar");
        }

        

        private bool SendHTTP(String url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool runPlayerWhenNotAlreadyStarted()
        {
            Process[] processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                if (!string.IsNullOrEmpty(proc.MainWindowTitle))
                    if (proc.ProcessName.Equals("foobar2000")){
                        Console.WriteLine("foobar found");
                        return true;
                    }
            }
            Console.WriteLine("foobar not found");

            Execute("");

            //String foo = @"X:\play.bat";
            //UdpClient c = new UdpClient("127.0.0.1", 5566);

            //c.Send(Encoding.ASCII.GetBytes(foo), foo.Length);
            //c.Close();
            // ProcessStartInfo startInfo = new ProcessStartInfo();
            // startInfo.FileName = @"X:\play.bat";
            
            //startInfo.RedirectStandardOutput = true;
            //startInfo.UseShellExecute = false;
            //Process.Start(startInfo);
            //Console.WriteLine("executed foobar");
        

            return false;
        }

    }
    
}
