using System;

namespace Demos.Screens
{
    public class AudioOptionsScreen : MenuScreen
    {
        public AudioOptionsScreen(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<OptionsScreen>);
        }
    }
}