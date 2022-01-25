using Kashkeshet.Common.Communicators;
using System.Collections.Generic;

namespace Kashkeshet.ServerSide.Core
{
    public interface IMessageHistory
    {
        IEnumerable<(object sender, object message, ChatProtocol protocol)> GetHistory();
        void AddToHistory((object, object, ChatProtocol) data);
    }
}
