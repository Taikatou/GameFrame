using System;
using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedMud<T> : IStateModifier<float>
    {
        public float Modifier => 3.0f;
    }
}
