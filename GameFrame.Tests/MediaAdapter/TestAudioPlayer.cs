using GameFrame.MediaAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace GameFrame.Tests.MediaAdapter
{
    [TestClass]
    public class TestAudioPlayer : GameFrameGame
    {

        public TestAudioPlayer()
        {
            var deviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            LoadContent();
        }

        [TestMethod]
        public void PlayWav()
        {
            IAudioPlayer player = new AudioAdapter();
            player.Play("wav", "BirabutoKingdom");
            player.Resume();
            player.Pause();
            player.Dispose();
        }

        [TestMethod]
        public void PlayMp3()
        {
            IAudioPlayer player = new AudioAdapter();
            player.Play("mp3", "piano");
            player.Resume();
            player.Pause();
            player.Dispose();
        }

        [TestMethod]
        public void PlayBadWav()
        {
            IAudioPlayer player = new AudioAdapter();
            player.Play("wav", "BirabutoKing");
            player.Resume();
            player.Pause();
            player.Dispose();
        }

        [TestMethod]
        public void PlayBadMp3()
        {
            IAudioPlayer player = new AudioAdapter();
            player.Play("mp3", "pianoooo");
            player.Resume();
            player.Pause();
            player.Dispose();
        }

        [TestMethod]
        public void ForceAudioPlayer()
        {
            IAudioPlayer player = new AudioPlayer();
            player.Play("mp3", "piano");
            player.Resume();
            player.Pause();
            player.Dispose();
        }
    }
}
