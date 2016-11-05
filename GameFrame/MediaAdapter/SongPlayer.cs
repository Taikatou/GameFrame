using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;
using GameFrame.Content;
using Microsoft.Xna.Framework.Content;

namespace GameFrame.MediaAdapter
{
    public class SongPlayer : IAdvancedAudioPlayer
    {

        private Song _song;

        public ContentManager Content { get; }

        public SongPlayer()
        {
            Content = ContentManagerFactory.RequestContentManager();
        }

        public void PlayAudio(string fileName)
        {
            try
            {
                _song = Content.Load<Song>(fileName);
                System.Diagnostics.Debug.WriteLine("SongPlayer::PlayAudio(): " + fileName);
                MediaPlayer.Play(_song);
                MediaPlayer.Volume = 100;
                MediaPlayer.IsRepeating = true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
            
        }

        public void PauseAudio()
        {
            MediaPlayer.Pause();
            Debug.WriteLine("SongPlayer::PauseAudio()");

        }

        public void ResumeAudio()
        {
            MediaPlayer.Resume();
            Debug.WriteLine("SongPlayer::ResumeAudio()");

        }

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
