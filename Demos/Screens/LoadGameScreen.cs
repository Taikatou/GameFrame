using System;

namespace Demos.Screens
{
    public class LoadGameScreen : MenuScreen
    {
        public LoadGameScreen(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<MainMenuScreen>);
        }
    }
}