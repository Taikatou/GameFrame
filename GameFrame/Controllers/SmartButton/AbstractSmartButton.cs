using System;

namespace GameFrame.Controllers.SmartButton
{
    public abstract class AbstractSmartButton
    {
        public event EventHandler OnButtonJustPressed;
        public event EventHandler OnButtonReleased;
        public event EventHandler OnButtonHeldDown;

        public abstract bool ButtonJustPressed { get; }
        public abstract bool ButtonReleased { get; }
        public abstract bool ButtonHeldDown { get; }

        public virtual void Update()
        {
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
