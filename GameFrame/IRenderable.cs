using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame
{
    public interface IRenderable
    {
        void Draw(SpriteBatch spriteBatch);
        Rectangle Area { get; }
    }
}
