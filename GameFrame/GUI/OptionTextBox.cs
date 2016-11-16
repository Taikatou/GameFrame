using System;
using Microsoft.Xna.Framework.Graphics;
using Ink.Runtime;

namespace GameFrame.GUI
{
    public class OptionTextBox : TextBox
    {
        public override string TextToShow => Pages[CurrentPage];
        public EventHandler OptionEvent;
        public int OptionIndex;
        public Choice Choice;

        public OptionTextBox(SpriteFont font, int optionIndex, Choice choice) : base(font)
        {
            Text = choice.text;
            OptionIndex = optionIndex;
            Choice = choice;
        }

        public override void Interact()
        {
            OptionEvent?.Invoke(this, null);
            Hide();
        }
    }
}
