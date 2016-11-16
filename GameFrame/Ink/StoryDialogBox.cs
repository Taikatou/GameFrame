using System.Collections.Generic;
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
        private readonly List<TextBox> _textBoxes;
        private readonly BaseMovable _player;
        private Vector2 _cachedPosition;
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private Story _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent;

        public StoryDialogBox(SpriteFont font, BaseMovable player)
        {
            _font = font;
            _textBoxes = new List<TextBox>();
            _player = player;
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
            StoryState = StoryState.Closed;
        }

        public void Update(GameTime gameTime)
        {
            if (StoryState != StoryState.Closed)
            {
                if (_cachedPosition != _player.Position)
                {
                    _textBoxes.Clear();
                }
                else if (_textBoxes.Count > 0)
                {
                    switch (StoryState)
                    {
                        case StoryState.Dialog:
                            if (!_textBoxes[0].Active)
                            {
                                LoadOptions();
                            }
                            break;
                        case StoryState.Option:
                            foreach (var option in _textBoxes)
                            {
                                var optionBox = option as OptionTextBox;
                                if (optionBox != null && !optionBox.Active)
                                {
                                    ChooseOption(optionBox);
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
        }

        public void ChooseOption(OptionTextBox option)
        {
            _textBoxes.Clear();
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
            _textBoxes.Clear();
            if (_activeStory.currentChoices.Count > 0)
            {
                var optionBoxes = new List<OptionTextBox>();
                var choices = _activeStory.currentChoices;
                for (var i = 0; i < choices.Count; i++)
                {
                    var option = new OptionTextBox(_font, i, choices[i]);
                    optionBoxes.Add(option);
                    option.Show();
                }
                OptionTextBoxFactory.LineTextBoxes(optionBoxes);
                foreach (var option in optionBoxes)
                {
                    _textBoxes.Add(option);
                }
            }
            StoryState = StoryState.Option;
        }

        public bool Interact(Point p)
        {
            foreach (var textBox in _textBoxes)
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
            var transformMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: transformMatrix);
            foreach (var textBox in _textBoxes)
            {
                textBox.Draw(spriteBatch);   
            }
            spriteBatch.End();
        }

        public void LoadDialogBox()
        {
            var text = _activeStory.ContinueMaximally();
            var dialogBox = new DialogBox(_font, text);
            dialogBox.DialogBoxEvent += s => DialogBoxEvent?.Invoke(_activeStory, s);
            dialogBox.Show();
            _textBoxes.Clear();
            _textBoxes.Add(dialogBox);
            _cachedPosition = _player.Position;
            StoryState = StoryState.Dialog;
        }

        public void AddDialogBox(Story story)
        {
            _activeStory = story;
            LoadDialogBox();
        }
    }
}
