using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public interface IAdvancedAudioPlayer
    {
        void PlayAudio(string audioType, string fileName);
        void ResumeAudio();
        void PauseAudio();
        void Dispose();
    }
}
