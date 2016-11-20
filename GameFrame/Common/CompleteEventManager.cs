using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class CompleteEventManager : IUpdate
    {
        private readonly List<CompleteEvent> _completeEvents;

        public CompleteEventManager()
        {
            _completeEvents = new List<CompleteEvent>();
        }

        public void AddCompleteEvent(CompleteEvent completeEvent)
        {
            _completeEvents.Add(completeEvent);
        }

        public void Update(GameTime gameTime)
        {
            var toRemove = new List<CompleteEvent>();
            foreach (var completeEvent in _completeEvents)
            {
                if (completeEvent.Complete)
                {
                    completeEvent.InvokeEvent();
                    toRemove.Add(completeEvent);
                }
            }
            foreach (var completeEvent in toRemove)
            {
                _completeEvents.Remove(completeEvent);
            }
        }
    }
}
