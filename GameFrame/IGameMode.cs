using System;
using MonoGame.Extended;

namespace GameFrame
{
    public interface IGameMode : IRenderable, IUpdate, IDisposable
    {
    }
}
