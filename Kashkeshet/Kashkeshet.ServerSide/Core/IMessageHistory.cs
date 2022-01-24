using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IMessageHistory
    {
        IEnumerable<object> GetHistory();
        void AddToHistory(object message);
    }
}
