using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.Abstractions
{
    public interface IProtocolOutputFactory
    {
        IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> CreateProtocolOutputHandler();
    }
}
