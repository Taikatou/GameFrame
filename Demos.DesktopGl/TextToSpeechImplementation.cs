using System;
using System.Speech.Synthesis;
using System.Windows.Controls;
using GameFrame.TextToSpeech;

namespace Demos.DesktopGl
{
    public class TextToSpeechImplementation : ITextToSpeech
    {
        public void Speak(string text)
        {
            using (var synth = new SpeechSynthesizer())
            {
                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();

                // Speak a string synchronously.
                synth.Speak(text);
            }
        }
    }
}