using System.Speech.Synthesis;
using GameFrame.TextToSpeech;

namespace Demos.DesktopGl
{
    public class TextToSpeechImplementation : ITextToSpeech
    {
        public void Speak(string text)
        {
            var synth = new SpeechSynthesizer();

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();
            synth.Rate = 0;
            synth.SelectVoiceByHints(VoiceGender.Male);
            // Speak a string asynchronously.
            synth.SpeakAsync(text);
        }
    }
}