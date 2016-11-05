namespace GameFrame.MediaAdapter
{
    public interface IAdvancedAudioPlayer
    {
        void PlayAudio(string fileName);
        void ResumeAudio();
        void PauseAudio();
        void Dispose();
    }
}
