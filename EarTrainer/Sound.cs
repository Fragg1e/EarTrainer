using System;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace EarTrainer
{ 
    public static class Sound
    {
        private static readonly WaveOutEvent output = new WaveOutEvent();
        private static FadeInOutSampleProvider musicFader;

        public static void PlaySound(string file)
        {
            Console.WriteLine("Playing sound: " + file);
            
            output.Stop();

            var reader = new AudioFileReader($@"..\..\sounds/{file}.wav"); //gets the audio file        

            musicFader = new FadeInOutSampleProvider(reader); //creates the fader

            reader.Volume = (float)Properties.Settings.Default.Volume;

            output.Init(musicFader); //initialises music 

            output.Play(); //starts it
            
            
        }
    }
}
