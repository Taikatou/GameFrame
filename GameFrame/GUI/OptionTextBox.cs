using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework.Graphics;
using Ink.Runtime;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.GUI
{
    public class OptionTextBox : TextBox
    {
        public override string TextToShow => Pages[CurrentPage];
        public int OptionIndex;
        public Choice Choice;
        private Color _choosenColor;
        private readonly Texture2D _choosenTexture;
        public virtual Color ChoosenColor
        {
            get { return _choosenColor; }
            set
            {
                _choosenColor = value;
                _choosenTexture.SetData(new[] { _choosenColor });
            }
        }

        public OptionTextBox(Size screenSize, SpriteFont font, int optionIndex, Choice choice) : base(screenSize, font)
        {
            Text = choice.text;
            OptionIndex = optionIndex;
            Choice = choice;
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _choosenTexture = new Texture2D(graphicsDevice, 1, 1);
            _choosenTexture.SetData(new[] { new Color(1.0f, 1.0f, 0.55f, 0.5f) });
        }

        public override void Interact()
        {
            InteractEvent?.Invoke(this, null);
        }

        public void Draw(SpriteBatch spriteBatch, bool choosen)
        {
            var texture = choosen ? _choosenTexture : FillTexture;
            Draw(spriteBatch, texture);
        }
    }
}
