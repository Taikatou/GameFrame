using System.IO;
using GameFrame.Services;

namespace Demos.iOS
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {
            throw new System.NotImplementedException();
        }

        public string LoadText(string filename)
        {
            var text = File.ReadAllText("TestData/ReadMe.txt");
            return text;
        }
    }
}
