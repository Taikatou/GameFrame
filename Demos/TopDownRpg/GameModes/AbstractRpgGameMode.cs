﻿using System;
using System.Collections.Generic;
using GameFrame;
using GameFrame.Controllers;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.GUI;
using GameFrame.Ink;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;

namespace Demos.TopDownRpg.GameModes
{
    public delegate void ClickEvent(Point p);
    public abstract class AbstractRpgGameMode : IGameMode
    {
        public List<IUpdate> UpdateList { get; }
        public StoryDialogBox DialogBox { get; set; }
        public GuiManager GuiManager { get; set; }
        public ClickEvent ClickEvent { get; set; }
        public EventHandler InteractEvent { get; set; }
        public ClickController ClickController { get; set; }
        private EventHandler _backButtonClickEvent;
        protected AbstractRpgGameMode(EventHandler clickEvent)
        {
            _backButtonClickEvent = clickEvent;
            GuiManager = new GuiManager();
            var backButton = new BackButtonGuiLayer
            {
                ClickEvent = clickEvent
            };
            GuiManager.AddGuiLayer(backButton);
            UpdateList = new List<IUpdate> { GuiManager };
            ClickController = new ClickController
            {
                MouseControl =
                {
                    OnPressedEvent = (state, mouseState) =>
                    {
                        if (!GuiManager.Interact(mouseState.Position))
                        {
                            ClickEvent?.Invoke(mouseState.Position);
                        }
                    }
                }
            };
            var moveGesture = new SmartGesture(GestureType.Tap)
            {
                GestureEvent = gesture =>
                {
                    var position = gesture.Position.ToPoint();
                    if (!GuiManager.Interact(position))
                    {
                        ClickEvent?.Invoke(position);
                    }
                }
            };
            ClickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(ClickController);
        }

        public void AddInteractionController()
        {
            var controller = new SmartController();
            var buttonsCreated = StaticServiceLocator.ContainsService<List<IButtonAble>>();
            if (!buttonsCreated)
            {
                var interactButton = new List<IButtonAble>
                {
                    new KeyButton(Keys.E),
                    new GamePadButton(Buttons.A)
                };
                StaticServiceLocator.AddService(interactButton);
            }
            var buttons = StaticServiceLocator.GetService<List<IButtonAble>>();
            var smartButton = new CompositeSmartButton(buttons)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    if (!DialogBox.Interact())
                    {
                        InteractEvent?.Invoke(this, null);
                    }
                }
            };
            controller.AddButton(smartButton);
            var upButton = new List<IButtonAble> {new DirectionGamePadButton(Buttons.DPadUp, PlayerIndex.One, false)};
            var smartUpButton = new CompositeSmartButton(upButton)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    DialogBox.Up();
                }
            };
            controller.AddButton(smartUpButton);
            var downButton = new List<IButtonAble> { new DirectionGamePadButton(Buttons.DPadDown, PlayerIndex.One, false) };
            var smartDownButton = new CompositeSmartButton(downButton)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    DialogBox.Down();
                }
            };
            controller.AddButton(smartDownButton);
            var optionsButtons = new List<IButtonAble>
            {
                new KeyButton(Keys.Escape),
                new GamePadButton(Buttons.Start)
            };
            var optionsAction = new CompositeSmartButton(optionsButtons)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    _backButtonClickEvent.Invoke(this, null);
                }
            };
            controller.AddButton(optionsAction);
            UpdateList.Add(controller);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var toUpdate in UpdateList)
            {
                toUpdate.Update(gameTime);
            }
        }

        public abstract void Dispose();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
