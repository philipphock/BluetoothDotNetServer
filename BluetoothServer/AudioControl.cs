using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;


namespace BluetoothServer
{
    class AudioControl
    {
        private static volatile AudioControl instance;
        private static object syncRoot = new Object();
        private readonly MMDevice multimediaDevice;

        private const int VOLRANGE = 64;
        

        private AudioControl() {

            MMDeviceEnumerator enumer = new MMDeviceEnumerator();
            multimediaDevice = enumer.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            
        }

        public static AudioControl Instance
        {
            get 
            {
                if (instance == null) 
                {
                lock (syncRoot) 
                {
                    if (instance == null)
                        instance = new AudioControl();
                }
                }

                return instance;
            }
        }


        public float GetMasterVolume(){          
            return multimediaDevice.AudioEndpointVolume.MasterVolumeLevel;
        }

        public float SetMasterVolume(float percent)
        {
            float volLvl = (float)(percent * AudioControl.VOLRANGE) - AudioControl.VOLRANGE;
            if (volLvl < -64 || volLvl > 0)
            {
                return multimediaDevice.AudioEndpointVolume.MasterVolumeLevel;

            }
            multimediaDevice.AudioEndpointVolume.MasterVolumeLevel = (int) volLvl;
            return multimediaDevice.AudioEndpointVolume.MasterVolumeLevel;
        }
            

        public bool ToggleMute()
        {
            multimediaDevice.AudioEndpointVolume.Mute = !multimediaDevice.AudioEndpointVolume.Mute;
            return multimediaDevice.AudioEndpointVolume.Mute;

        }

        

    }

  
}
