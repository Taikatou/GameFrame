using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg.Factory
{
    public class EntityFactoryA : AbstractEntityFactory
    {
        public override EntityController MakeEntityController(Entity entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            return EntityController.CreateEntityController(entity, possibleMovements, moverManager);
        }

        public override EntityRenderer MakeEntityRenderer(ContentManager content,
            ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            var entityRenderer = EntityRenderer.CreateEntityRenderer(content, spaitalHash, entity, tileSize);
            return entityRenderer;
        }
    }
}
