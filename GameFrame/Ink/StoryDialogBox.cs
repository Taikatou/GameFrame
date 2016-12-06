using System.Collections.Generic;
using GameFrame.GUI;
using GameFrame.Renderers;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

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
        private ITextBox _currentTextBox;
        private readonly Camera2D _camera;
        private readonly SpriteFont _font;
        private GameFrameStory _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent { get; set; }
        public Size ScreenSize;

        public StoryDialogBox(Size screenSize, SpriteFont font)
        {
            ScreenSize = screenSize;
            _font = font;
            var graphicsDevice = StaticServiceLocator.GetService<BoxingViewportAdapter>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
            StoryState = StoryState.Closed;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void EndDialog()
        {
            StoryState = StoryState.Closed;
        }

        public void ChooseOption(OptionTextBox option)
        {
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
                var optionBoxes = new List<OptionTextBox>();
                var choices = _activeStory.Choices;
                for (var i = 0; i < choices.Count; i++)
                {
                    var option = new OptionTextBox(ScreenSize, _font, i, choices[i]);
                    optionBoxes.Add(option);
                    option.Show();
                    option.InteractEvent += (sender, args) => ChooseOption(option);
                }
                OptionTextBoxFactory.LineTextBoxes(optionBoxes, ScreenSize);
                _currentTextBox = new OptionTextBoxList(optionBoxes);
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
                _currentTextBox.Interact();
            }
            return dialogOpen;
        }

        public bool Interact(Point p)
        {
            var point = _camera.ScreenToWorld(p.ToVector2());
            return _currentTextBox != null && _currentTextBox.Interact(point.ToPoint());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Complete)
            {
                var transformMatrix = _camera.GetViewMatrix();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: transformMatrix);
                _currentTextBox.Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        public void LoadDialogBox()
        {
            if (!_activeStory.Complete)
            {
                var storyText = _activeStory.CurrentText;
                var dialogBox = new DialogBox(ScreenSize, _font, storyText);
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
                _currentTextBox = dialogBox;
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
