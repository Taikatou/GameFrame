using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public class Mp4Player : IAdvancedMediaPlayer
    {
        public void play(string fileName)
        {
            System.Diagnostics.Debug.WriteLine("playing VLC with mp4");
        }

        public void playMp4(string filename)
        {
            System.Diagnostics.Debug.WriteLine("playing MP4 with mp4");
        }
    }
}
