using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos
{
    public abstract  class AbstractScene : IUpdate
    {
        public List<IUpdate> UpdateList;
        public abstract void LoadScene();
        public abstract void Draw(SpriteBatch spriteBatch);

        protected AbstractScene()
        {
            UpdateList = new List<IUpdate>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var element in UpdateList)
            {
                element.Update(gameTime);
            }
        }
    }
}
