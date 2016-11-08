using System;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public class AudioOptionsScreen : MenuScreen
    {
        public AudioOptionsScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider) 
            : base(viewPort, serviceProvider)
        {
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Back", Show<OptionsScreen>);
        }
    }
}