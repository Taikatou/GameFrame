using Microsoft.Xna.Framework.Content;

namespace GameFrame.Content
{
    public class ContentManagerFactory
    {
        private static ContentManagerFactory _instance;

        private readonly ContentManager _baseManager;

        protected ContentManagerFactory(ContentManager baseManager)
        {
            _baseManager = baseManager;
        }

        public static ContentManager RequestContentManager()
        {
            var baseContent = _instance._baseManager;
            var newManager = new ContentManager(baseContent.ServiceProvider, baseContent.RootDirectory);
            return newManager;
        }

        public static void Initialise(ContentManager content)
        {
            _instance = new ContentManagerFactory(content);
        }
    }
}
