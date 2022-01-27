using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.Abstractions
{
    public interface IClientFactory
    {
        IClientRunnable CreateClientReceiver(IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> protocolOutputHandler);
        IClientRunnable CreateClientSender(IDictionary<ChatProtocol, Action<ICommunicator>> protocolHandler);
        IClient CreateChatClient(IClientRunnable sender, IClientRunnable receiver, int serverPort, string serverIP);
        ICommunicator CreateCommunicator(int serverPort, string serverIP);
    }
}
