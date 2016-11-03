using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrame.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameFrame.MediaAdapter
{
    public class AudioPlayer : IAdvancedAudioPlayer
    {
        ContentManager _content;
        private SoundEffect _effect;
        private SoundEffectInstance _instance;


        public void PlayAudio(string audioType, string fileName)
        {
            if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("AudioPlayer::play(): " + fileName);
                    _content = ContentManagerFactory.RequestContentManager();
                    _effect = _content.Load<SoundEffect>(fileName);
                    _instance = _effect.CreateInstance();
                    _instance.Play();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            else
            {
                Debug.WriteLine("Invalid media. " + audioType + " format not supported");
            }
        }

        public void PauseAudio()
        {
            _instance?.Pause();
            Debug.WriteLine("AudioPLayer::PauseAudio()");
        }

        public void ResumeAudio()
        {
            Debug.WriteLine("AudioPLayer::ResumeAudio()");
        }

        public void Dispose()
        {
            Debug.WriteLine("AudioPLayer::Dispose()");
            _content.Unload();
        }
    }
}
