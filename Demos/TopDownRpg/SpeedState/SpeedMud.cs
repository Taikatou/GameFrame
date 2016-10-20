using System;
using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedMud<T> : IState<float>
    {
        public float Modifier => 3.0f;
    }
}
