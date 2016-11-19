using System;
using System.Collections.Generic;
using System.Diagnostics;
using GameFrame.GUI;
using GameFrame.Movers;
using GameFrame.Renderers;
using GameFrame.ServiceLocator;
using Ink.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameFrame.Ink
{
    public enum StoryState
    {
        Dialog,
        Option,
        Closed
    }
    public delegate void StoryDialogBoxEvent(Story story, string text);
    public class StoryDialogBox : IUpdate
    {
        public StoryState StoryState;
        private readonly List<TextBox> _activeBoxes;
        private readonly BaseMovable _player;
        private Vector2 _cachedPosition;
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private Story _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent;

        public StoryDialogBox(SpriteFont font, BaseMovable player)
        {
            _font = font;
            _activeBoxes = new List<TextBox>();
            _player = player;
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
            StoryState = StoryState.Closed;
        }

        public void Update(GameTime gameTime)
        {
            if (StoryState != StoryState.Closed && _cachedPosition != _player.Position)
            {
                _activeBoxes.Clear();
            }
        }

        public void ChooseOption(OptionTextBox option)
        {
            _activeBoxes.Clear();
            _activeStory.ChooseChoiceIndex(option.OptionIndex);
            if (_activeStory.canContinue)
            {
                LoadDialogBox();
            }
            else
            {
                StoryState = StoryState.Closed;
            }
        }

        public void LoadOptions()
        {
            _activeBoxes.Clear();
            if (_activeStory.currentChoices.Count > 0)
            {
                var optionBoxes = new List<OptionTextBox>();
                var choices = _activeStory.currentChoices;
                for (var i = 0; i < choices.Count; i++)
                {
                    var option = new OptionTextBox(_font, i, choices[i]);
                    optionBoxes.Add(option);
                    option.Show();
                    option.InteractEvent += (sender, args) => ChooseOption(option);
                }
                OptionTextBoxFactory.LineTextBoxes(optionBoxes);
                foreach (var option in optionBoxes)
                {
                    _activeBoxes.Add(option);
                }
                StoryState = StoryState.Option;
            }
            else
            {
                StoryState = StoryState.Closed;
            }
        }

        public bool Interact()
        {
            var dialogOpen = StoryState == StoryState.Dialog;
            if (dialogOpen)
            {
                _activeBoxes[0].Interact();
            }
            return dialogOpen;
        }

        public bool Interact(Point p)
        {
            foreach (var textBox in _activeBoxes)
            {
                var hit = textBox.TextRectangle.Contains(p);
                var valid = hit && textBox.Active;
                if (valid)
                {
                    textBox.Interact();
                    return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (StoryState != StoryState.Closed)
            {
                var transformMatrix = _camera.GetViewMatrix();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: transformMatrix);
                foreach (var textBox in _activeBoxes)
                {
                    textBox.Draw(spriteBatch);
                }
                spriteBatch.End();
            }
        }

        public void LoadDialogBox()
        {
            if (_activeStory.canContinue)
            {
                var text = _activeStory.ContinueMaximally();
                var dialogBox = new DialogBox(_font, text);
                dialogBox.DialogBoxEvent += s => DialogBoxEvent?.Invoke(_activeStory, s);
                dialogBox.Show();
                dialogBox.InteractEvent += (sender, args) => LoadOptions();
                _activeBoxes.Clear();
                _activeBoxes.Add(dialogBox);
                _cachedPosition = _player.Position;
                StoryState = StoryState.Dialog;
            }
            else
            {
                StoryState = StoryState.Closed;
            }
        }

        public void AddDialogBox(Story story)
        {
            _activeStory = story;
            LoadDialogBox();
        }
    }
}
