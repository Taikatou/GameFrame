using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg.Factory
{
    public abstract class AbstractEntityFactory
    {
        public abstract EntityController MakeEntityController(Entity entity, IPossibleMovements possibleMovements,
            MoverManager moverManager);
        public abstract EntityRenderer MakeEntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> expiringSpatialHashCollisionSystem, Entity entity, Point spaitalHash);
    }
}
