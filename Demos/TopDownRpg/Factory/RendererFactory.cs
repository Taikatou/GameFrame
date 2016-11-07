using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg.Factory
{
    public abstract class RendererFactory
    {
        public abstract EntityRenderer CreateEntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> expiringSpatialHashCollisionSystem, Entity entity, Point spaitalHash);
    }
}
