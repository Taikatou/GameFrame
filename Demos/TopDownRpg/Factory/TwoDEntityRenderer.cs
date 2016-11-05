using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg.Factory
{
    public class TwoDEntityRenderer : RendererFactory
    {
        public override EntityRenderer CreateEntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            return new EntityRenderer(content, spaitalHash, entity, tileSize);
        }
    }
}
