using System;

namespace GameFrame.MediaAdapter
{
    public class AudioAdapter : AudioPlayer
    {
        public SongPlayer SongPlayer { get; set; }

        public override void Play(string audioType, string fileName)
        {
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                SongPlayer = new SongPlayer();
                SongPlayer.PlayAudio(fileName);
            }
            else if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                base.Play("wav",fileName);
            }
        }

        public override void Pause()
        {
            SongPlayer?.PauseAudio();
            base.Pause();
        }

       
        public override void Resume()
        {
            SongPlayer?.ResumeAudio();
            base.Resume();
        }
    }
}
