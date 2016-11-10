using System;
using Demos.TopDownRpg;
using Microsoft.Xna.Framework;
using Demos.Puzzle;
using Demos.Pong;
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

            AddMenuItem("New Game", Show<TopDownRpgScene>);
            AddMenuItem("Pong", Show<PongScreen>);
            AddMenuItem("Load Game", Show<LoadGameScreen>);
            AddMenuItem("Options", Show<OptionsScreen>);
            AddMenuItem("Exit", _game.Exit);
        }
    }
}
