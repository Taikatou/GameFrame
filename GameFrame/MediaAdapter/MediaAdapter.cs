using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public class MediaAdapter : IMediaPlayer
    {
        public IAdvancedMediaPlayer advancedMediaPlayer { get; set; }

        public MediaAdapter() { }

        public void play(string audioType, string fileName)
        {
            if (audioType.Equals("wav", StringComparison.OrdinalIgnoreCase))
            {
                advancedMediaPlayer = new SongPlayer();
                advancedMediaPlayer.play(fileName);
            }
            else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                advancedMediaPlayer = new Mp4Player();
                advancedMediaPlayer.play(fileName);
            }
        }
    }
}
