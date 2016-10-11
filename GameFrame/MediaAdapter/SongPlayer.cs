using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using GameFrame.Content;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameFrame.MediaAdapter
{
    public class SongPlayer : IAdvancedMediaPlayer
    {
        Song song { get; set; }
        ContentManager content;

        public SongPlayer()
        {
            content = ContentManagerFactory.RequestContentManager();
        }

        public void play(string fileName)
        {
            content.Load<Song>("Ghost-in-the-house");
            System.Diagnostics.Debug.WriteLine("playing wav with soudnplayer");
        }

        public void Dispose()
        {
            content.Unload();
        }
    }
}
