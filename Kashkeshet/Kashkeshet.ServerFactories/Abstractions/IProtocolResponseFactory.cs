using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.ChatImplementation;
using System.Collections.Generic;

namespace Kashkeshet.ServerFactories.Abstractions
{
    public interface IProtocolResponseFactory
    {
        IDictionary<ChatProtocol, ProtocolAction> CreateResponseHandler();
    }
}
