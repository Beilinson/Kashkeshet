using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    [Serializable]
    public class LongTermHistory : IMessageHistory
    {
        private readonly ICollection<(object sender, object message, ChatProtocol protocol)> _history;

        public LongTermHistory(ICollection<(object, object, ChatProtocol)> history)
        {
            _history = history;
        }

        public void AddToHistory((object, object, ChatProtocol) data)
        {
            _history.Add(data);
        }

        public IEnumerable<(object sender, object message, ChatProtocol protocol)> GetHistory()
        {
            return _history;
        }
    }
}
