using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleEntityRenderer
    {
        private readonly Texture2D _entityTexture;
        public Rectangle Area { get; }
        public Rectangle FrameRectangle { get; }
        public BattleEntityRenderer(Rectangle size, Entity entity, ContentManager content)
        {
            _entityTexture = content.Load<Texture2D>($"TopDownRpg/{entity.SpriteSheet}");
            FrameRectangle = size;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_entityTexture, position, FrameRectangle, Color.White);
        }
    }
}
