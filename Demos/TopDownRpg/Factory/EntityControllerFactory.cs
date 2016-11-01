using System;
using System.Collections.Generic;
using Demos.TopDownRpg.SpeedState;
using GameFrame.Controllers;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Demos.TopDownRpg.Factory
{
    public class EntityControllerFactory : SinglePlayerControllerFactory
    {
        public override EntityController CreateEntityController(BaseMovable moveable, IPossibleMovements possibleMovements, MoverManager moverManager)
        {
            var controller = base.CreateEntityController(moveable, possibleMovements, moverManager);
            var runningButton = new List<IButtonAble> { new KeyButton(Keys.B), new GamePadButton(Buttons.B) };
            var entity = moveable as Entity;
            if (entity != null)
            {
                var smartButton = new CompositeSmartButton(runningButton)
                {
                    OnButtonJustPressed = (sender, args) => { entity.SpeedContext.SetSpeed(new SpeedRunning()); },
                    OnButtonReleased = (sender, args) => { entity.SpeedContext.SetSpeed(new SpeedNormal()); }
                };
                controller.AddButton(smartButton);
            }
            return controller;
        }
    }
}
