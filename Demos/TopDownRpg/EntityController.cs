using System.Collections.Generic;
using GameFrame.Controllers;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace Demos.TopDownRpg
{
    public class EntityController : IUpdate
    {
        private readonly EntityRenderer _entity;
        private readonly SmartController _smartController;

        public EntityController(EntityRenderer entity, IMover entityMover)
        {
            _entity = entity;
            _smartController = new SmartController();
            var upButtons = new List<IButtonAble> { new KeyButton(Keys.W), new KeyButton(Keys.Up), new GamePadButton(Buttons.DPadUp) };
            CreateCompositeButton(upButtons, entityMover, new Vector2(0, -1));

            var downButtons = new List<IButtonAble> { new KeyButton(Keys.S), new KeyButton(Keys.Down), new GamePadButton(Buttons.DPadDown) };
            CreateCompositeButton(downButtons, entityMover, new Vector2(0, 1));

            var leftButtons = new List<IButtonAble> { new KeyButton(Keys.A), new KeyButton(Keys.Left), new GamePadButton(Buttons.DPadLeft) };
            CreateCompositeButton(leftButtons, entityMover, new Vector2(-1, 0));

            var rightButtons = new List<IButtonAble> { new KeyButton(Keys.D), new KeyButton(Keys.Right), new GamePadButton(Buttons.DPadRight) };
            CreateCompositeButton(rightButtons, entityMover, new Vector2(1, 0));
        }

        public void CreateCompositeButton(List<IButtonAble> buttons, IMover entityMover, Vector2 direction)
        {
            var smartButton = new CompositeSmartButton();
            foreach (var button in buttons)
            {
                smartButton.AddButton(button);
            }
            smartButton.OnButtonJustPressed = (sender, args) =>
            {
                _entity.Walking = true;
                var position = _entity.Position + direction;
                entityMover.RequestMovement(position);
            };
            smartButton.OnButtonHeldDown = (sender, args) =>
            {
                var position = _entity.Position + direction;
                entityMover.RequestMovement(position);
            };
            smartButton.OnButtonReleased = (sender, args) =>
            {
                _entity.Walking = false;
            };
            _smartController.AddButton(smartButton);
        }

        public void Update(GameTime gameTime)
        {
            _smartController.Update(gameTime);
        }
    }
}
