using Microsoft.Xna.Framework.Graphics;
using Ink.Runtime;
using Microsoft.Xna.Framework;

namespace GameFrame.GUI
{
    public class OptionTextBox : TextBox
    {
        public override string TextToShow => Pages[CurrentPage];
        public int OptionIndex;
        public Choice Choice;

        public OptionTextBox(SpriteFont font, int optionIndex, Choice choice) : base(font)
        {
            Text = choice.text;
            OptionIndex = optionIndex;
            Choice = choice;
            FillColor = new Color(1.0f, 1.0f, 0.55f, 0.5f);
        }

        public override void Interact()
        {
            InteractEvent?.Invoke(this, null);
        }
    }
}
