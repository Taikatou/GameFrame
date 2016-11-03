using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;

namespace GameFrame.Controllers
{
    public abstract class ControllerFactory
    {
        public abstract BaseMovableController CreateEntityController(AbstractMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager);
    }
}
