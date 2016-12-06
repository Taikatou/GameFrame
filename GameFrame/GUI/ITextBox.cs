using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.GUI
{
    public interface ITextBox
    {
        void Interact();
        bool Interact(Point p);
        void Draw(SpriteBatch spriteBatch);
    }
}
