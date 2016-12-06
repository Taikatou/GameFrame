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
        public IControllerSettings ControllerSettings;
        public SinglePlayerControllerFactory()
        {
            ControllerSettings = StaticServiceLocator.GetService<IControllerSettings>();
        }

        public void AddKeyBoardButton(List<IButtonAble> buttonList, Keys key)
        {
            if (ControllerSettings.KeyBoardMouseEnabled)
            {
                buttonList.Add(new KeyButton(key));
            }
        }
        public void AddGamePadButton(List<IButtonAble> buttonList, Buttons button)
        {
            if (ControllerSettings.KeyBoardMouseEnabled)
            {
                buttonList.Add(new GamePadButton(button));
            }
        }
        public override BaseMovableController CreateEntityController(BaseMovable entity, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            var directions = new Dictionary<BaseMovableController.Directions, List<IButtonAble>>
            {
                [BaseMovableController.Directions.Down] = new List<IButtonAble>(),
                [BaseMovableController.Directions.Up] = new List<IButtonAble>(),
                [BaseMovableController.Directions.Left] = new List<IButtonAble>(),
                [BaseMovableController.Directions.Right] = new List<IButtonAble>()
            };
            if (ControllerSettings.GamePadEnabled)
            {
                directions[BaseMovableController.Directions.Down].Add(new DirectionGamePadButton(Buttons.DPadDown));
                directions[BaseMovableController.Directions.Left].Add(new DirectionGamePadButton(Buttons.DPadLeft));
                directions[BaseMovableController.Directions.Up].Add(new DirectionGamePadButton(Buttons.DPadUp));
                directions[BaseMovableController.Directions.Right].Add(new DirectionGamePadButton(Buttons.DPadRight));
            }
            if (ControllerSettings.KeyBoardMouseEnabled)
            {
                directions[BaseMovableController.Directions.Down].Add(new KeyButton(Keys.S));
                directions[BaseMovableController.Directions.Down].Add(new KeyButton(Keys.Down));
                directions[BaseMovableController.Directions.Left].Add(new KeyButton(Keys.A));
                directions[BaseMovableController.Directions.Left].Add(new KeyButton(Keys.Left));
                directions[BaseMovableController.Directions.Up].Add(new KeyButton(Keys.Up));
                directions[BaseMovableController.Directions.Up].Add(new KeyButton(Keys.W));
                directions[BaseMovableController.Directions.Right].Add(new KeyButton(Keys.D));
                directions[BaseMovableController.Directions.Right].Add(new KeyButton(Keys.Right));
            }
            return new BaseMovableController(entity, possibleMovements, moverManager, directions);
        }
    }
}
