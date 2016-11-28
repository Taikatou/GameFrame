using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Demos.TopDownRpg
{
    public class PlayerEntityRenderer : AbstractEntityRenderer
    {
        public PlayerEntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize) : base(content, spaitalHash, entity, tileSize)
        {
        }

        public override Rectangle FrameRectangle => DirectionMapper.GetRectangle(TileSize, Entity.FacingDirection.ToPoint());
    }
}
