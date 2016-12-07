using System;
using GameFrame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.GUI
{
    public class BackButtonGuiLayer : IGuiLayer, IDisposable
    {
        private readonly ImageButton _backButton;
        private readonly ContentManager _content;
        public EventHandler ClickEvent;
        public BackButtonGuiLayer()
        {
            _content = ContentManagerFactory.RequestContentManager();
            var buttonTexture = _content.Load<Texture2D>("DemoScreen/BackButton");
            var buttonSize = new Rectangle(15, 15, 45, 45);
            _backButton = new ImageButton(buttonTexture, buttonSize);
        }

        public void Update(GameTime gameTime)
        {
        }

        public bool Interact(Point p)
        {
            var hit = _backButton.Hit(p);
            if (hit)
            {
                ClickEvent?.Invoke(this, null);
            }
            return hit;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _backButton.Draw(spriteBatch);
        }

        public void Dispose()
        {
            _content.Unload();
            _content.Dispose();
        }
    }
}
