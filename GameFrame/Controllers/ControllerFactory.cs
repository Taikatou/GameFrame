using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.Controllers
{
    public abstract class ControllerFactory
    {
        public abstract BaseMovableController CreateEntityController(BaseMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager, Vector2 movementCircle);
    }
}
