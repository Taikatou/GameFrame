namespace GameFrame.Controllers.SmartButton
{
    class AlternativeSmartButton : AbstractSmartButton
    {
        private readonly IButtonAble _button;
        private readonly IButtonAble _alternativeButton;
        public override bool ButtonJustPressed => ButtonMethods.ButtonJustPressed(_button) ||
                                                  ButtonMethods.ButtonJustPressed(_alternativeButton);
        public override bool ButtonReleased => ButtonMethods.ButtonReleased(_button) ||
                                               ButtonMethods.ButtonReleased(_alternativeButton);
        public override bool ButtonHeldDown => ButtonMethods.ButtonHeldDown(_button) ||
                                               ButtonMethods.ButtonHeldDown(_alternativeButton);
        public AlternativeSmartButton(IButtonAble button, IButtonAble alternativeButton)
        {
            _button = button;
            _alternativeButton = alternativeButton;
        }

        public override void Update()
        {
            _button.Update();
            _alternativeButton.Update();
            base.Update();
        }
    }
}
