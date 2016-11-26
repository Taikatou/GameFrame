using System.Collections.Generic;
using GameFrame;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.Ink;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;

namespace Demos.TopDownRpg.GameModes
{
    public delegate void ClickEvent(Point p);
    public abstract class AbstractRpgGameMode : IGameMode
    {
        public List<IUpdate> UpdateList { get; }
        public StoryDialogBox DialogBox { get; set; }
        public ClickEvent ClickEvent { get; set; }

        protected AbstractRpgGameMode()
        {
            UpdateList = new List<IUpdate>();
            var clickController = new ClickController();
            clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                if (!DialogBox.Interact(mouseState.Position))
                {
                    ClickEvent?.Invoke(mouseState.Position);
                }
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                var position = gesture.Position.ToPoint();
                if (!DialogBox.Interact(position))
                {
                    ClickEvent?.Invoke(position);
                }
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var toUpdate in UpdateList)
            {
                toUpdate.Update(gameTime);
            }
        }

        public abstract void Dispose();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
