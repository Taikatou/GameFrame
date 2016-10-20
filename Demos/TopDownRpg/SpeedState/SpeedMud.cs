using System;
using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedMud<T> : IState<int>
    {
        public int Modifier => 900;
    }
}
