using System;
using System.IO;
using GameFrame.Services;

namespace Demos.UWP
{
    public class SaveAndLoad : ISaveAndLoad
    {
        public void SaveText(string filename, string text)
        {

        }
        public string LoadText(string filename)
        {
            var text = File.ReadAllText(filename);
            return text;
        }
    }
}
