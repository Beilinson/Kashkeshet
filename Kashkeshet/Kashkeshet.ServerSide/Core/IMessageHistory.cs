using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IMessageHistory
    {
        IEnumerable<(object sender, object message)> GetHistory();
        void AddToHistory(object sender, object message);
    }
}
