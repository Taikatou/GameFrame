using System;
using System.Collections.Generic;
using GameFrame.Content;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public abstract class MenuScreen : Screen
    {
        private readonly IServiceProvider _serviceProvider;
        private SpriteBatch _spriteBatch;
        public List<MenuItem> MenuItems { get; }
        protected BitmapFont Font { get; private set; }
        private ContentManager _content;
        private readonly ClickController _clickController;
        private readonly Camera2D _camera;

        protected MenuScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider)
        {
            _camera = new Camera2D(viewPort) { Zoom = 1.0f };
            _serviceProvider = serviceProvider;
            MenuItems = new List<MenuItem>();

            _clickController = new ClickController();
            _clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                CheckClick(mouseState.Position.ToVector2());
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                CheckClick(gesture.Position);
            };
            _clickController.TouchScreenControl.AddSmartGesture(moveGesture);
        }

        protected void AddMenuItem(string text, Action action)
        {
            var menuItem = new MenuItem(Font, text)
            {
                Position = new Vector2(300, 200 + 32 * MenuItems.Count),
                Action = action
            };

            MenuItems.Add(menuItem);
        }

        public override void Dispose()
        {
            base.Dispose();

            _spriteBatch.Dispose();
        }

        public override void LoadContent()
        {
            base.LoadContent();

            var graphicsDeviceService = (IGraphicsDeviceService)_serviceProvider.GetService(typeof(IGraphicsDeviceService));
            _content = ContentManagerFactory.RequestContentManager();
            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            Font = _content.Load<BitmapFont>("montserrat-32");
        }

        public override void UnloadContent()
        {
            _content.Unload();
            _content.Dispose();

            base.UnloadContent();
        }

        public void CheckClick(Vector2 point)
        {
            point = _camera.ScreenToWorld(point);
            foreach (var menuItem in MenuItems)
            {
                var isClicked = menuItem.BoundingRectangle.Contains(point);

                if (isClicked)
                {
                    menuItem.Action?.Invoke();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _clickController.Update(gameTime);
            var mouseState = Mouse.GetState();
            foreach (var menuItem in MenuItems)
            {
                var isHovered = menuItem.BoundingRectangle.Contains(mouseState.X, mouseState.Y);
                menuItem.Color = isHovered ? Color.Yellow : Color.White;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);

            foreach (var menuItem in MenuItems)
                menuItem.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}