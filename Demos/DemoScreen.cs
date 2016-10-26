using Demos.Screens;
using GameFrame;
using GameFrame.Content;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Diagnostics;

namespace Demos
{
    public class DemoScreen : GameFrameScreen
    {
        public readonly ContentManager Content;
        public readonly Camera2D Camera;
        public readonly SpriteBatch SpriteBatch;
        private BackButton _backButton;
       // private Entity _entity;
        //private Vector2 _tileSize;

        public DemoScreen(ViewportAdapter viewPort, SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
            Content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort) { Zoom = 2.0f };
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _backButton = new BackButton(Content);


            var clickController = new ClickController();
            clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                CheckHit(mouseState.Position);
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                CheckHit(gesture.Position.ToPoint());
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
        }

        public void CheckHit(Point point)
        {
            Debug.WriteLine("" + point);
            if (_backButton.Hit(point))
            {
                Action action = Show<MainMenuScreen>;
                action.Invoke();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            _backButton.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
