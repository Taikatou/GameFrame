using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;

namespace GameFrame.Controllers
{
    public class SinglePlayerControllerFactory : ControllerFactory
    {
        public override EntityController CreateEntityController(BaseMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            return new EntityController(entity, possibleMovements, moverManager);
        }
    }
}
