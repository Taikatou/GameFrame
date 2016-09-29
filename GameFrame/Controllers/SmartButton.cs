using System;

namespace GameFrame.Controllers
{
    public class SmartButton
    {
        private readonly IButtonAble _button;
        public event EventHandler OnButtonJustPressed;
        public event EventHandler OnButtonReleased;
        public event EventHandler OnButtonHeldDown;

        public bool ButtonJustPressed => _button.Active && !_button.PreviouslyActive;
        public bool ButtonReleased => !_button.Active && _button.PreviouslyActive;
        public bool ButtonHeldDown => _button.Active && _button.PreviouslyActive;

        public SmartButton(IButtonAble button)
        {
            _button = button;
        }

        public void Update()
        {
            _button.Update();
            // Just 
            if(ButtonJustPressed)
            {
                OnButtonJustPressed?.Invoke(this, null);
            }
            if(ButtonReleased)
            {
                OnButtonReleased?.Invoke(this, null);
            }
            if(ButtonHeldDown)
            {
                OnButtonHeldDown?.Invoke(this, null);
            }
        }
    }
}
