using System;

namespace GameFrame.MediaAdapter
{
    public class AudioAdapter : IAudioPlayer
    {
        public AudioPlayer AudioPlayer
        {get; set; }

        public SongPlayer SongPlayer { get; set; }

        public void Play(string audioType, string fileName)
        {
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                SongPlayer = new SongPlayer();
                SongPlayer.PlayAudio(fileName);
            }
            else if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                AudioPlayer = new AudioPlayer();
                AudioPlayer.Play("wav",fileName);
            }
        }

        public void Pause()
        {
            SongPlayer?.PauseAudio();
            AudioPlayer?.Pause();
        }

       
        public void Resume()
        {
            SongPlayer?.ResumeAudio();
            AudioPlayer?.Resume();
        }
    }
}
