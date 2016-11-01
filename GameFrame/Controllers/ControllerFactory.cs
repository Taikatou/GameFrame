using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;

namespace GameFrame.Controllers
{
    public abstract class ControllerFactory
    {
        public abstract EntityController CreateEntityController(BaseMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager);
    }
}
