using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class LongTermHistory : IMessageHistory
    {
        private ICollection<object> _history;

        public LongTermHistory(ICollection<object> history)
        {
            _history = history;
        }

        public void AddToHistory(object message)
        {
            _history.Add(message);
        }

        public IEnumerable<object> GetHistory()
        {
            return _history;
        }
    }
}
