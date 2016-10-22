using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace GameFrame
{
    public abstract class GameFrameScreen : Screen
    {
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;

        protected GameFrameScreen()
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsVisible)
            {
                foreach (var element in UpdateList)
                {
                    element.Update(gameTime);
                }
            }
        }
    }
}
