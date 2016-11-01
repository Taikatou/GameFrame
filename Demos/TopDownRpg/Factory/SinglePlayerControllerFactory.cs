using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrame.Controllers;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;

namespace Demos.TopDownRpg.Factory
{
    public class SinglePlayerControllerFactory : ControllerFactory
    {
        public override EntityController CreateEntityController(Entity entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            return new EntityController(entity, possibleMovements, moverManager);
        }
    }
}
