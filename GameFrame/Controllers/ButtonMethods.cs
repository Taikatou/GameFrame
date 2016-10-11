namespace GameFrame.Controllers
{
    public class ButtonMethods
    {
        public static bool ButtonJustPressed(IButtonAble button)
        {
            return button.Active && !button.PreviouslyActive;
        }

        public static bool ButtonReleased(IButtonAble button)
        {
            return !button.Active && button.PreviouslyActive;
        }

        public static bool ButtonHeldDown(IButtonAble button)
        {
            return button.Active && button.PreviouslyActive;
        }
    }
}
