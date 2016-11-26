using System.Collections.Generic;
using GameFrame.GUI;
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
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private GameFrameStory _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent { get; set; }

        public StoryDialogBox(SpriteFont font)
        {
            _font = font;
            _activeBoxes = new List<TextBox>();
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
            StoryState = StoryState.Closed;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void EndDialog()
        {
            _activeBoxes.Clear();
            StoryState = StoryState.Closed;
        }

        public void ChooseOption(OptionTextBox option)
        {
            _activeBoxes.Clear();
            _activeStory.ChooseChoiceIndex(option.OptionIndex);
            if (!_activeStory.Complete)
            {
                _activeStory.Continue();
                LoadDialogBox();
            }
            else
            {
                EndDialog();
            }
        }

        public void LoadOptions()
        {
            if (_activeStory.CanContinue)
            {
                LoadDialogBox();
            }
            else if (_activeStory.Choices.Count > 0)
            {
                _activeBoxes.Clear();
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
                EndDialog();
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
                    textBox.Draw(spriteBatch, _camera.Zoom);
                }
                spriteBatch.End();
            }
        }

        public void LoadDialogBox()
        {
            _activeBoxes.Clear();
            if (!_activeStory.Complete)
            {
                var storyText = _activeStory.CurrentText;
                var dialogBox = new DialogBox(_font, storyText);
                dialogBox.InteractEvent += (sender, args) =>
                {
                    if (_activeStory.CanContinue)
                    {
                        _activeStory.Continue();
                        LoadDialogBox();
                    }
                    else
                    {
                        LoadOptions();
                    }
                };
                dialogBox.Show();
                _activeBoxes.Add(dialogBox);
                StoryState = StoryState.Dialog;
            }
            else
            {
                EndDialog();
            }
        }

        public virtual void StartStory(GameFrameStory story)
        {
            _activeStory = story;
            LoadDialogBox();
        }
    }
}
