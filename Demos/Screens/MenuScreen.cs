using System;
using System.Collections.Generic;
using GameFrame;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended.BitmapFonts;

namespace Demos.Screens
{
    public abstract class MenuScreen : GameFrameScreen
    {
        private readonly IServiceProvider _serviceProvider;
        private SpriteBatch _spriteBatch;
        public List<MenuItem> MenuItems { get; }
        protected BitmapFont Font { get; private set; }
        protected ContentManager Content { get; private set; }

        protected MenuScreen(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            MenuItems = new List<MenuItem>();

            var clickController = new ClickController();
            clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                CheckClick(mouseState.Position);
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                CheckClick(gesture.Position.ToPoint());
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
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

        public override void Initialize()
        {
            base.Initialize();

            Content = new ContentManager(_serviceProvider, "Content");
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

            _spriteBatch = new SpriteBatch(graphicsDeviceService.GraphicsDevice);
            Font = Content.Load<BitmapFont>("montserrat-32");
        }

        public override void UnloadContent()
        {
            Content.Unload();
            Content.Dispose();

            base.UnloadContent();
        }

        public void CheckClick(Point point)
        {
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

            _spriteBatch.Begin();

            foreach (var menuItem in MenuItems)
                menuItem.Draw(_spriteBatch);

            _spriteBatch.End();
        }
    }
}