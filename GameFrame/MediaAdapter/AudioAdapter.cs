using System;
using System.Diagnostics;

namespace GameFrame.MediaAdapter
{
    public class AudioAdapter : IAudioPlayer
    {
        public AudioPlayer AudioPlayer { get; set; }
        public SongPlayer SongPlayer { get; set; }

        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                SongPlayer = new SongPlayer();
                SongPlayer.Play(fileName);
            }
            else if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                AudioPlayer = new AudioPlayer();
                AudioPlayer.PlayAudio("wav",fileName);
            }
        }

        public void Pause()
        {
            SongPlayer?.Pause();
            AudioPlayer?.PauseAudio();
        }

       
        public void Resume()
        {
            SongPlayer?.Resume();
            AudioPlayer?.ResumeAudio();
        }

        public void Dispose()
        {
            Debug.WriteLine("AudioAdapter::Dispose()");
            SongPlayer?.Dispose();
            AudioPlayer?.Dispose();
        }
    }
}
