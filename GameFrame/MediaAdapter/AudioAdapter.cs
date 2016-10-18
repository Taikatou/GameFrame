using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SongPlayer.Play(fileName);
            }
            else if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                AudioPlayer = new AudioPlayer();
                AudioPlayer.Play("wav",fileName);
            }
        }

        public void Pause()
        {
            SongPlayer?.Pause();
            AudioPlayer?.Pause();
        }

       
        public void Resume()
        {
            SongPlayer?.Resume();
            AudioPlayer?.Resume();
        }
    }
}
