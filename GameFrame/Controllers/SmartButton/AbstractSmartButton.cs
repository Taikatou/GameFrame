using System;
using System.Diagnostics;

namespace GameFrame.Controllers.SmartButton
{
    public abstract class AbstractSmartButton
    {
        public EventHandler OnButtonJustPressed;
        public EventHandler OnButtonReleased;
        public EventHandler OnButtonHeldDown;

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
