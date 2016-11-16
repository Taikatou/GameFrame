using System;
using System.Diagnostics;
using GameFrame.GUI;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Renderers
{
    public delegate void DialogBoxEvent(string text);
    public class DialogBox : TextBox
    {
        private int _interval;
        public override string TextToShow => Pages[CurrentPage];

        public Stopwatch Stopwatch;
        public DialogBoxEvent DialogBoxEvent;
        public DialogBox(SpriteFont font, string text) : base(font)
        {
            Text = text;
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            var centerScreen = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            var posX = centerScreen.X - (Size.X / 2f);
            var posY = graphicsDevice.Viewport.Height - Size.Y - 30;
            Position = new Vector2(posX, posY);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (Active)
            {
                // Draw a blinking indicator to guide the player through to the next page
                // This stops blinking on the last page
                // NOTE: You probably want to use an image here instead of a string
                if (BlinkIndicator() || CurrentPage == Pages.Count - 1)
                {
                    var indicatorPosition = new Vector2(TextRectangle.X + TextRectangle.Width - CharacterSize.X - 4,
                        TextRectangle.Y + TextRectangle.Height - (CharacterSize.Y));

                    spriteBatch.DrawString(Font, ">", indicatorPosition, Color.Red);
                }
            }
        }

        public override void Interact()
        {
            if (Active)
            {
                if (CurrentPage >= Pages.Count - 1)
                {
                    Hide();
                }
                else
                {
                    CurrentPage++;
                    Stopwatch.Restart();
                    DialogBoxEvent?.Invoke(TextToShow);
                }
            }
        }

        public override void Show()
        {
            base.Show();
            // use stopwatch to manage blinking indicator
            Stopwatch = new Stopwatch();
            DialogBoxEvent?.Invoke(TextToShow);
            Stopwatch.Start();
        }

        public override void Hide()
        {
            base.Hide();

            Stopwatch.Stop();
            Stopwatch = null;
        }

        private bool BlinkIndicator()
        {
            _interval = (int)Math.Floor((double)(Stopwatch.ElapsedMilliseconds % 1000));

            return _interval < 500;
        }
    }
}
