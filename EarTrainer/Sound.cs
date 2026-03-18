using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace EarTrainer
{
    public static class Sound
    {
        private static WaveOutEvent output = new WaveOutEvent();
        private static FadeInOutSampleProvider musicFader;

        public static void PlaySound(string file)
        {
            output.Stop();
            var reader = new AudioFileReader($@"..\..\sounds/{file}.wav"); //gets the audio file        

            musicFader = new FadeInOutSampleProvider(reader); //creates the fader

            reader.Volume = 0.7f;

            output.Init(musicFader); //initialises music 

            output.Play(); //starts it 
            
        }
    }
}
