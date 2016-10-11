using System.Collections.Generic;

namespace GameFrame.Controllers.SmartButton
{
    public class CompositeSmartButton : AbstractSmartButton
    {
        private readonly List<IButtonAble> _buttons;

        public override bool ButtonJustPressed
        {
            get
            {
                foreach (var button in _buttons)
                {
                    if (ButtonMethods.ButtonJustPressed(button))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override bool ButtonReleased
        {
            get
            {
                foreach (var button in _buttons)
                {
                    if (ButtonMethods.ButtonReleased(button))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override bool ButtonHeldDown
        {
            get
            {
                foreach (var button in _buttons)
                {
                    if (ButtonMethods.ButtonHeldDown(button))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public CompositeSmartButton()
        {
            _buttons = new List<IButtonAble>();
        }

        public void AddButton(IButtonAble button)
        {
            _buttons.Add(button);
        }

        public override void Update()
        {
            foreach (var button in _buttons)
            {
                button.Update();
            }
            base.Update();
        }
    }
}
