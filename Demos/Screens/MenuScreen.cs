using System;
using System.Collections.Generic;
using GameFrame.Content;
using GameFrame.Controllers;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
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
        private int _selected;
        private readonly SmartController _controller;
        public int Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value >= 0 && value < MenuItems.Count)
                {
                    _selected = value;
                }
            }
        }

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
            var upButtons = new List<IButtonAble>
            {
                new DirectionGamePadButton(Buttons.DPadUp),
                new KeyButton(Keys.W),
                new KeyButton(Keys.Up)
            };
            var downButtons = new List<IButtonAble>
            {
                new DirectionGamePadButton(Buttons.DPadDown),
                new KeyButton(Keys.S),
                new KeyButton(Keys.Down)
            };
            var actionButtons = new List<IButtonAble>
            {
                new GamePadButton(Buttons.A),
                new KeyButton(Keys.E)
            };
            var upAction = new CompositeSmartButton(upButtons)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    Selected--;
                }
            };
            var downAction = new CompositeSmartButton(downButtons)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    Selected++;
                }
            };
            var action = new CompositeSmartButton(actionButtons)
            {
                OnButtonReleased = (sender, args) =>
                {
                    MenuItems[Selected].Action.Invoke();
                }
            };
            _controller = new SmartController();
            _controller.AddButton(upAction);
            _controller.AddButton(downAction);
            _controller.AddButton(action);
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
            _controller.Update(gameTime);
            _clickController.Update(gameTime);
            var mouseState = Mouse.GetState();
            for(var i = 0; i < MenuItems.Count; i++)
            {
                var isHovered = MenuItems[i].BoundingRectangle.Contains(mouseState.X, mouseState.Y);
                if (isHovered)
                {
                    MenuItems[i].Color = Color.Yellow;
                    Selected = i;
                }
                else
                {
                    MenuItems[i].Color = Color.White;
                }
            }
            MenuItems[Selected].Color = Color.Yellow;
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