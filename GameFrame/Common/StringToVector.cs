using System;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class StringToVector
    {
        public static Vector2 ConvertString(string text)
        {
            text = text.Replace(" ", "");
            var split = text.Split(',');
            var v = new Vector2(Convert.ToInt32(split[0]), Convert.ToInt32(split[1]));
            return v;
        }
    }
}
