using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;

namespace GameFrame.MediaAdapter
{
    public class SongPlayer : IAdvancedMediaPlayer
    {
        Song song { get; set; }

        public void play(string fileName)
        {
       
            System.Diagnostics.Debug.WriteLine("playing wav with soudnplayer");
        }
    }
}
