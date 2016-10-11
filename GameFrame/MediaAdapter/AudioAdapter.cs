using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public class AudioAdapter : IAudioPlayer
    {
        public AudioPlayer _audioPlayer
        {get; set; }

        public SongPlayer SongPlayer { get; set; }

        public void play(string audioType, string fileName)
        {
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                SongPlayer = new SongPlayer();
                SongPlayer.play(fileName);
            }
            else if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                _audioPlayer = new AudioPlayer();
                _audioPlayer.play("wav",fileName);
            }
        }

        public void pause()
        {
            SongPlayer?.pause();
            _audioPlayer?.pause();
        }

       
        public void resume()
        {
            SongPlayer?.resume();
            _audioPlayer?.resume();
        }
    }
}
