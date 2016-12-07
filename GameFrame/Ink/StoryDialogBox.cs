using System.Collections.Generic;
using GameFrame.GUI;
using GameFrame.Renderers;
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
    public class StoryDialogBox : IGuiLayer, ICompleteAble
    {
        public bool Complete => StoryState == StoryState.Closed;
        public StoryState StoryState;
        private ITextBox _currentTextBox;
        private readonly SpriteFont _font;
        private GameFrameStory _activeStory;
        public StoryDialogBoxEvent DialogBoxEvent { get; set; }
        public Size ScreenSize;
        private readonly bool _gamePad;

        public StoryDialogBox(Size screenSize, SpriteFont font, bool gamePad)
        {
            _gamePad = gamePad;
            ScreenSize = screenSize;
            _font = font;
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
                _currentTextBox = new OptionTextBoxList(optionBoxes, _gamePad);
                StoryState = StoryState.Option;
            }
            else
            {
                EndDialog();
            }
        }

        public bool Interact()
        {
            var dialogOpen = StoryState != StoryState.Closed;
            if (dialogOpen)
            {
                _currentTextBox.Interact();
            }
            return dialogOpen;
        }

        public bool Interact(Point point)
        {
            return _currentTextBox != null && _currentTextBox.Interact(point);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Complete)
            {
                _currentTextBox.Draw(spriteBatch);
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

        public void Down()
        {
            MoveOption(1);
        }

        public void Up()
        {
            MoveOption(-1);
        }

        public void MoveOption(int valueBy)
        {
            if (StoryState == StoryState.Option)
            {
                var optionBox = _currentTextBox as OptionTextBoxList;
                optionBox?.MoveOption(valueBy);
            }
        }
    }
}
