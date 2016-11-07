using GameFrame.Content;
using Microsoft.Xna.Framework;

namespace GameFrame
{
    public class GameFrameGame : Game
    {
        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            ContentManagerFactory.Initialise(Content);
        }
    }
}
