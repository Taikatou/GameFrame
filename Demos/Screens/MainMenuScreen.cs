using System;
using Demos.TopDownRpg;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private readonly Game _game;

        public MainMenuScreen(ViewportAdapter viewPort, IServiceProvider serviceProvider, Game game)
            : base(viewPort, serviceProvider)
        {
            _game = game;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            AddMenuItem("Play Game", Show<TopDownRpgScene>);
        }
    }
}
