using System.Collections.Generic;
using Android.Speech.Tts;
using GameFrame.TextToSpeech;

namespace Demos.Droid
{
    public class TextToSpeechImplementation : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        private TextToSpeech _speaker;
        private string _toSpeak;

        public void Speak(string text)
        {
            var ctx = Android.App.Application.Context; // useful for many Android SDK features
            _toSpeak = text;
            if (_speaker == null)
            {
                _speaker = new TextToSpeech(ctx, this);
            }
            else
            {
                var p = new Dictionary<string, string>();
#pragma warning disable 618
                _speaker.Speak(_toSpeak, QueueMode.Flush, p);
#pragma warning restore 618
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                var p = new Dictionary<string, string>();
#pragma warning disable 618
                _speaker.Speak(_toSpeak, QueueMode.Flush, p);
#pragma warning restore 618
            }
        }
        #endregion
    }
}