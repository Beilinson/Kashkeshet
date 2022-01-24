using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class LongTermHistory : IMessageHistory
    {
        private ICollection<(object sender, object message)> _history;

        public LongTermHistory(ICollection<(object, object)> history)
        {
            _history = history;
        }

        public void AddToHistory(object sender, object message)
        {
            _history.Add((sender, message));
        }

        public IEnumerable<(object sender, object message)> GetHistory()
        {
            return _history;
        }
    }
}
