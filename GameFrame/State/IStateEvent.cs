using System;

namespace GameFrame.State
{
    public interface IStateEvent
    {
        EventHandler Event { get; }
    }
}
