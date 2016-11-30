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
using MonoGame.Extended.ViewportAdapters;
using System;
using GameFrame.GUI;

namespace Demos
{
    public class DemoScreen : GameFrameScreen
    {
        public readonly ContentManager Content;
        private readonly Camera2D _camera;
        public readonly SpriteBatch SpriteBatch;
        private ImageButton _backButton;
        private ClickController _clickController;

        public DemoScreen(ViewportAdapter viewPort, SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
            Content = ContentManagerFactory.RequestContentManager();
            _camera = new Camera2D(viewPort) { Zoom = 1.0f };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _clickController.Update(gameTime);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var buttonTexture = Content.Load<Texture2D>("DemoScreen/BackButton");
            var buttonSize = new Rectangle(15, 15, 45, 45);
            _backButton = new ImageButton(buttonTexture, buttonSize);

            _clickController = new ClickController();
            _clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                CheckHit(mouseState.Position.ToVector2());
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                CheckHit(gesture.Position);
            };
            _clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            _clickController.AddSmartGesture(moveGesture);
        }

        public void CheckHit(Vector2 point)
        {
            var screenPoint = _camera.ScreenToWorld(point);
            if (_backButton.Hit(screenPoint.ToPoint()))
            {
                Action action = Show<MainMenuScreen>;
                action.Invoke();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = _camera.GetViewMatrix();
            SpriteBatch.Begin(transformMatrix: transformMatrix);
            _backButton.Draw(SpriteBatch);
            SpriteBatch.End();
        }
    }
}
