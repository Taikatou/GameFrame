using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg
{
    public class EntityRenderer : AbstractEntityRenderer
    {
        public EntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize) : base(content, spaitalHash, entity, tileSize)
        {
            FrameRectangle = new Rectangle(new Point(), tileSize);
        }

        public override Rectangle FrameRectangle { get; }
    }
}
