using System.Collections.Generic;
using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace Demos
{
    public abstract class AbstractScreen : Screen
    {
        public List<IUpdate> UpdateList;
        public abstract void LoadScene();
        public abstract void Draw(SpriteBatch spriteBatch);

        protected AbstractScreen()
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var element in UpdateList)
            {
                element.Update(gameTime);
            }
        }
    }
}
