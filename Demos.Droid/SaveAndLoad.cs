using System;
using System.IO;
using Android.Content.Res;
using GameFrame.Services;

namespace Demos.Droid
{
    public class SaveAndLoad : ISaveAndLoad
    {
        private AssetManager _assets;

        public SaveAndLoad(AssetManager assets)
        {
            _assets = assets;
        }

        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.CurrentDirectory;
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, text);
        }

        public string LoadText(string filename)
        {
            var toReturn = "";
            using (var sr = new StreamReader(_assets.Open(filename)))
            {
                toReturn += sr.ReadToEnd();
            }
            return toReturn;
        }
    }
}
