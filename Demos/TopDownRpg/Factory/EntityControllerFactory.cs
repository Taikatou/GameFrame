﻿using System.Collections.Generic;
using Demos.TopDownRpg.SpeedState;
using GameFrame.Controllers;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Demos.TopDownRpg.Factory
{
    public class EntityControllerFactory : SinglePlayerControllerFactory
    {
        public override BaseMovableController CreateEntityController(BaseMovable moveable, IPossibleMovements possibleMovements, MoverManager moverManager, Vector2 movementCircle)
        {
            var controller = base.CreateEntityController(moveable, possibleMovements, moverManager, movementCircle);
            var runningButton = new List<IButtonAble>();
            AddKeyBoardButton(runningButton, Keys.B);
            AddGamePadButton(runningButton, Buttons.B);
            var entity = moveable as Entity;
            if (entity != null)
            {
                var smartButton = new CompositeSmartButton(runningButton)
                {
                    OnButtonJustPressed = (sender, args) => { entity.SpeedContext.SpeedState = new SpeedRunning(); },
                    OnButtonReleased = (sender, args) => { entity.SpeedContext.SpeedState = new SpeedNormal(); }
                };
                controller.AddButton(smartButton);
            }
            return controller;
        }
    }
}
