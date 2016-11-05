using System.Collections.Generic;
using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class RoundingDictionary <T>
    {
        private readonly Point _roundBy;
        public readonly Dictionary<Point, T> Dictionary;

        public RoundingDictionary(Point roundBy)
        {
            _roundBy = roundBy;
            Dictionary = new Dictionary<Point, T>();
        }

        private Point Round(Point p)
        {
            return p - new Point(p.X%_roundBy.X, p.Y % _roundBy.Y);
        }

        public T this[Point key]
        {
            get
            {
                return Dictionary[Round(key)];
            }
            set
            {
                Dictionary[Round(key)] = value;
            }
        }

        public void Remove(Point p)
        {
            Dictionary.Remove(Round(p));
        }

        public bool ContainsKey(Point p)
        {
            return Dictionary.ContainsKey(Round(p));
        }
    }
}
