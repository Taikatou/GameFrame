using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Demos
{
    public interface IDemoScene
    {
        void LoadScene();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch _spriteBatch);
    }
}
