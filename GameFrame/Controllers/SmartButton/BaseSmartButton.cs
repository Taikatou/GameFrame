namespace GameFrame.Controllers.SmartButton
{
    public class BaseSmartButton : AbstractSmartButton
    {
        private readonly IButtonAble _button;
        public override bool ButtonJustPressed => ButtonMethods.ButtonJustPressed(_button);
        public override bool ButtonReleased => ButtonMethods.ButtonReleased(_button);
        public override bool ButtonHeldDown => ButtonMethods.ButtonHeldDown(_button);
        public BaseSmartButton(IButtonAble button)
        {
            _button = button;
        }

        public override void Update()
        {
            _button.Update();
            base.Update();
        }
    }
}
