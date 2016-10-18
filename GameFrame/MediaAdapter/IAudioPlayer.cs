using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public interface IAudioPlayer 
    {
        void Play(string audioType, string fileName);
        void Pause();
        void Resume();
    }
}
