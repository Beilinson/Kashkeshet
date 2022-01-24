using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IRoutable
    {
        IMessageHistory MessageHistory { get; }
        void UpdateHistory();
    }
}
