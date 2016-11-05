namespace GameFrame.MediaAdapter
{
    public interface IAudioPlayer 
    {
        void Play(string audioType, string fileName);
        void Pause();
        void Resume();
        void Dispose();
    }
}
