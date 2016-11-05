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

            _screenComponent.Register(new MainMenuScreen(Services, this));
            _screenComponent.Register(new LoadGameScreen(Services));
            _screenComponent.Register(new OptionsScreen(Services));
            _screenComponent.Register(new AudioOptionsScreen(Services));
            _screenComponent.Register(new VideoOptionsScreen(Services));
            _screenComponent.Register(new KeyboardOptionsScreen(Services));
            _screenComponent.Register(new MouseOptionsScreen(Services));
            _screenComponent.Register(new PuzzleScreen());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenSize.Width, ScreenSize.Height);
            var demoScene = new TopDownRpgScene(viewportAdapter, _spriteBatch);
            var pongScene = new PongScreen(viewportAdapter, _spriteBatch);
            pongScene.LoadContent();
            demoScene.LoadContent();
            _screenComponent.Register(demoScene);
            _screenComponent.Register(pongScene);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
