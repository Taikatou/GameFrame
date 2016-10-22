using System.Collections.Generic;
using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos
{
    public abstract  class AbstractScene : IUpdate
    {
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        public abstract void LoadScene();
        public abstract void Draw(SpriteBatch spriteBatch);

        protected AbstractScene()
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
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
