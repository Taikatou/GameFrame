using System.Collections.Generic;
using GameFrame.GUI;
using GameFrame.Movers;
using GameFrame.Renderers;
using GameFrame.ServiceLocator;
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
    public delegate void StoryDialogBoxEvent(GameFrameStory story, string text);
    public class StoryDialogBox : IUpdate, ICompleteAble
    {
        public bool Complete => StoryState == StoryState.Closed;
        public StoryState StoryState;
        private readonly List<TextBox> _activeBoxes;
        private readonly BaseMovable _player;
        private Vector2 _cachedPosition;
        private BaseMovable _interactWith;
        private Vector2 _interactWithCachedPosition;
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private GameFrameStory _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent { get; set; }

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
            if (!Complete && (_cachedPosition != _player.Position || 
                              _interactWithCachedPosition != _interactWith.Position))
            {
                _activeBoxes.Clear();
                StoryState = StoryState.Closed;
            }
        }

        public void ChooseOption(OptionTextBox option)
        {
            _activeBoxes.Clear();
            _activeStory.ChooseChoiceIndex(option.OptionIndex);
            if (!_activeStory.Complete)
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
            if (_activeStory.CanContinue)
            {
                LoadDialogBox();
            }
            else if (_activeStory.Choices.Count > 0)
            {
                var optionBoxes = new List<OptionTextBox>();
                var choices = _activeStory.Choices;
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
            if (!Complete)
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
            if (!_activeStory.Complete)
            {
                var storyText = _activeStory.CurrentText;
                var dialogBox = new DialogBox(_font, storyText);
                dialogBox.InteractEvent += (sender, args) =>
                {
                    if (_activeStory.CanContinue)
                    {
                        _activeStory.Continue();
                        _activeBoxes.Clear();
                        LoadDialogBox();
                    }
                    else
                    {
                        LoadOptions();
                    }
                };
                dialogBox.Show();
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

        public void AddDialogBox(GameFrameStory story, BaseMovable interactWith)
        {
            _activeStory = story;
            LoadDialogBox();
            _interactWith = interactWith;
            _interactWithCachedPosition = interactWith.Position;
        }
    }
}
