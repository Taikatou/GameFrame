using System.Collections.Generic;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers
{
    public class SinglePlayerControllerFactory : ControllerFactory
    {
        public override EntityController CreateEntityController(BaseMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            var controlllerSettings = StaticServiceLocator.GetService<IControllerSettings>();
            var directions = new Dictionary<EntityController.Directions, List<IButtonAble>>
            {
                [EntityController.Directions.Down] = new List<IButtonAble>(),
                [EntityController.Directions.Up] = new List<IButtonAble>(),
                [EntityController.Directions.Left] = new List<IButtonAble>(),
                [EntityController.Directions.Left] = new List<IButtonAble>(),
                [EntityController.Directions.Right] = new List<IButtonAble>()
            };
            if (controlllerSettings.GamePadEnabled)
            {
                directions[EntityController.Directions.Down].Add(new DirectionGamePadButton(Buttons.DPadUp));
                directions[EntityController.Directions.Left].Add(new DirectionGamePadButton(Buttons.DPadUp));
                directions[EntityController.Directions.Up].Add(new DirectionGamePadButton(Buttons.DPadUp));
                directions[EntityController.Directions.Right].Add(new DirectionGamePadButton(Buttons.DPadUp));
            }
            if (controlllerSettings.KeyBoardMouseEnabled)
            {
                directions[EntityController.Directions.Down].Add(new KeyButton(Keys.W));
                directions[EntityController.Directions.Down].Add(new KeyButton(Keys.Up));
                directions[EntityController.Directions.Left].Add(new KeyButton(Keys.L));
                directions[EntityController.Directions.Left].Add(new KeyButton(Keys.Left));
                directions[EntityController.Directions.Up].Add(new KeyButton(Keys.Up));
                directions[EntityController.Directions.Up].Add(new KeyButton(Keys.W));
                directions[EntityController.Directions.Right].Add(new KeyButton(Keys.D));
                directions[EntityController.Directions.Right].Add(new KeyButton(Keys.Right));
            }
            return new EntityController(entity, possibleMovements, moverManager, directions);
        }
    }
}
