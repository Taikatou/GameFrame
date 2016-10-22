using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg
{
    public class EntityRendererFactory
    {
        public static EntityRenderer MakeEntityRenderer(Point point, ContentManager content,
                                              ExpiringSpatialHashCollisionSystem<Entity> expiringSpatialHash,
                                              Vector2 tileSize )
        {
            var entity = new Entity(new Vector2(5, 5));
            var entityRenderer = new EntityRenderer(content, expiringSpatialHash, entity, tileSize.ToPoint());
            return entityRenderer;
        }
    }
}
