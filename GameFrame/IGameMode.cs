using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameFrame
{
    public interface IGameMode : IUpdate, IDisposable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
