using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using GameFrame.Content;
using Microsoft.Xna.Framework.Content;

namespace GameFrame.MediaAdapter
{
    public class SongPlayer { 

        private Song _song { get; set; }

        public ContentManager Content { get; }

        public SongPlayer()
        {
            Content = ContentManagerFactory.RequestContentManager();
        }

        public void play(string fileName)
        {
            _song = Content.Load<Song>(fileName);
            System.Diagnostics.Debug.WriteLine("SongPlayer::play(): " + fileName);
            MediaPlayer.Play(_song);
            MediaPlayer.Volume = 100;
            MediaPlayer.IsRepeating = true;
        }

        public void pause()
        {
            MediaPlayer.Pause();
            Debug.WriteLine("SongPlayer::Pause()");

        }

        public void resume()
        {
            MediaPlayer.Resume();
            Debug.WriteLine("SongPlayer::Resume()");

        }

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
