using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameFrame.GUI
{
    public interface IGuiLayer : IUpdate
    {
        bool Interact(Point p);
        void Draw(SpriteBatch spriteBatch);
    }
}
