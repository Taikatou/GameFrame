using System.Collections.Generic;
using Demos.Common;
using Demos.Pong;
using Demos.Puzzle;
using Demos.Screens;
using Demos.TopDownRpg;
using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

namespace Demos
{
    public class DemoGame : GameFrameGame
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenComponent _screenComponent;

        public DemoGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Components.Add(_screenComponent = new ScreenComponent(this));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenSize.Width, ScreenSize.Height);
            var screens = new List<Screen>
            {
                new MainMenuScreen(viewportAdapter, Services, this),
                new LoadGameScreen(viewportAdapter, Services),
                new OptionsScreen(viewportAdapter, Services),
                new AudioOptionsScreen(viewportAdapter, Services),
                new VideoOptionsScreen(viewportAdapter, Services),
                new KeyboardOptionsScreen(viewportAdapter, Services),
                new MouseOptionsScreen(viewportAdapter, Services),
                new TopDownRpgScene(viewportAdapter, _spriteBatch),
                new PongScreen(viewportAdapter, _spriteBatch)
            };
            foreach (var screen in screens)
            {
                screen.LoadContent();
                _screenComponent.Register(screen);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
