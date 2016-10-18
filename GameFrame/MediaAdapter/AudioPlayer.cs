using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public class AudioPlayer : IMediaPlayer
    {
        MediaAdapter mediaAdapter;

        public void play(string audioType, string fileName)
        {
            //inbuilt support to play mp3 music files
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine("Playing mp3 file. Name: " + fileName);
            }

            //mediaAdapter is providing support to play other file formats
            else if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase) 
                || audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.play(audioType, fileName);
            }

            else
            {
                Debug.WriteLine("Invalid media. " + audioType + " format not supported");
            }
        }
    }
}
