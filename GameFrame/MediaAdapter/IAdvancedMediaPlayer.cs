using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.MediaAdapter
{
    public interface IAdvancedAudioPlayer
    {
        void play(string fileName);
        void resume();
        void pause();
    }
}
