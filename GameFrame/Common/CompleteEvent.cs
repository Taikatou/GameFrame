using System;

namespace GameFrame.Common
{
    public class CompleteEvent : ICompleteAble
    {
        public EventHandler Event;
        private readonly ICompleteAble _completeAble;
        public bool Complete => _completeAble.Complete;
        public bool TriggeredBefore;

        public CompleteEvent(ICompleteAble completeAble)
        {
            _completeAble = completeAble;
        }

        public void InvokeEvent()
        {
            Event?.Invoke(this, null);
        }
    }
}
