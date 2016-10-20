using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace Demos
{
    public abstract class AbstractScreen : Screen
    {
        public List<IUpdate> UpdateList;
        public abstract void Draw(SpriteBatch spriteBatch);

        protected AbstractScreen()
        {
            UpdateList = new List<IUpdate>();
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
