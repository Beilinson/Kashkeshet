using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;

namespace Kashkeshet.ClientFactories.Abstractions
{
    public interface IProtocolRequestFactory
    {
        IDictionary<ChatProtocol, Action<ICommunicator>> CreateProtocolRequestHandler();
    }
}
