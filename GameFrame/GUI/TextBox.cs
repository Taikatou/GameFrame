using System;
using System.Collections.Generic;
using System.Text;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.GUI
{
    public class TextBox
    {
        public int CurrentPage;
        public readonly SpriteFont Font;
        public string Text { get; set; }
        public bool Active { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        private Color _fillColor;
        public Color FillColor
        {
            get { return _fillColor; }
            set
            {
                _fillColor = value;
                _fillTexture.SetData(new[] { _fillColor });
            }
        }
        public Color BorderColor { get; set; }
        public Color DialogColor { get; set; }
        public int BorderWidth { get; set; }
        private readonly Texture2D _fillTexture;
        private readonly Texture2D _borderTexture;
        public List<string> Pages;

        public virtual string TextToShow => Text;

        public const float DialogBoxMargin = 24f;
        public Rectangle TextRectangle => new Rectangle(Position.ToPoint(), Size.ToPoint());

        public Vector2 CharacterSize { get; set; }
        private int MaxCharsPerLine => (int)Math.Floor((Size.X - DialogBoxMargin) / CharacterSize.X);
        private int MaxLines => (int)Math.Floor((Size.Y - DialogBoxMargin) / CharacterSize.Y) - 1;
        public EventHandler InteractEvent { get; set; }

        private IEnumerable<Rectangle> BorderRectangles => new List<Rectangle>
        {
            // Top (contains top-left & top-right corners)
            new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y - BorderWidth,
                TextRectangle.Width + BorderWidth*2, BorderWidth),

            // Right
            new Rectangle(TextRectangle.X + TextRectangle.Size.X, TextRectangle.Y, BorderWidth, TextRectangle.Height),

            // Bottom (contains bottom-left & bottom-right corners)
            new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y + TextRectangle.Size.Y,
                TextRectangle.Width + BorderWidth*2, BorderWidth),

            // Left
            new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y, BorderWidth, TextRectangle.Height)
        };

        private Vector2 TextPosition => new Vector2(Position.X + DialogBoxMargin / 2, Position.Y + DialogBoxMargin / 2);

        public TextBox(Size screensize, SpriteFont font)
        {
            Font = font;
            CharacterSize = font.MeasureString(new StringBuilder("W", 1));
            Pages = new List<string>();
            BorderWidth = 2;
            DialogColor = Color.Black;

            BorderColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _fillTexture = new Texture2D(graphicsDevice, 1, 1);

            _borderTexture = new Texture2D(graphicsDevice, 1, 1);
            _borderTexture.SetData(new[] { BorderColor });
            var viewPort = StaticServiceLocator.GetService<BoxingViewportAdapter>();
            var sizeX = (int)(screensize.Width * 0.5);
            var sizeY = (int)(screensize.Height * 0.2);

            Size = new Vector2(sizeX, sizeY);
        }

        public virtual void Initialize(string text = null)
        {
            Text = text ?? Text;

            CurrentPage = 0;
            Show();
        }

        public virtual void Show()
        {
            Active = true;
            Pages = WordWrap(Text);
        }

        public virtual void Hide()
        {
            Active = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                // Draw each side of the border rectangle
                foreach (var side in BorderRectangles)
                {
                    spriteBatch.Draw(_borderTexture, null, side);
                }

                // Draw background fill texture (in this example, it's 50% transparent white)
                spriteBatch.Draw(_fillTexture, null, TextRectangle);

                // Draw the current page onto the dialog box
                spriteBatch.DrawString(Font, TextToShow, TextPosition, DialogColor);
            }
        }

        public virtual void Interact() { }

        public List<string> WordWrap(string text)
        {
            var pages = new List<string>();

            var capacity = MaxCharsPerLine * MaxLines > text.Length ? text.Length : MaxCharsPerLine * MaxLines;

            var result = new StringBuilder(capacity);
            var resultLines = 0;

            var currentWord = new StringBuilder();
            var currentLine = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var currentChar = text[i];
                var isNewLine = text[i] == '\n';
                var isLastChar = i == text.Length - 1;

                currentWord.Append(currentChar);

                if (char.IsWhiteSpace(currentChar) || isLastChar)
                {
                    var potentialLength = currentLine.Length + currentWord.Length;

                    if (potentialLength > MaxCharsPerLine)
                    {
                        result.AppendLine(currentLine.ToString());

                        currentLine.Clear();

                        resultLines++;
                    }

                    currentLine.Append(currentWord);

                    currentWord.Clear();

                    if (isLastChar || isNewLine)
                    {
                        result.AppendLine(currentLine.ToString());
                    }

                    if (resultLines > MaxLines || isLastChar || isNewLine)
                    {
                        pages.Add(result.ToString());

                        result.Clear();

                        resultLines = 0;

                        if (isNewLine)
                        {
                            currentLine.Clear();
                        }
                    }
                }
            }

            return pages;
        }
    }
}
