using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleEntityRenderer
    {
        private readonly Texture2D _entityTexture;
        public Rectangle SourceRectangle;
        public Rectangle FrameRectangle { get; }

        public BattleEntityRenderer(Rectangle destinationRectangle, Rectangle sourceRectangle, Entity entity, ContentManager content)
        {
            _entityTexture = content.Load<Texture2D>($"TopDownRpg/{entity.SpriteSheet}");
            FrameRectangle = destinationRectangle;
            SourceRectangle = sourceRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_entityTexture, FrameRectangle, SourceRectangle, Color.White);
        }
    }
}
