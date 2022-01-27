using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.Abstractions
{
    public interface IClientFactory
    {
        IClientRunnable CreateClientReceiver();
        IClientRunnable CreateClientSender(IDictionary<ChatProtocol, Action<ICommunicator>> protocolHandler);
        IClient CreateChatClient(IClientRunnable receiver, IClientRunnable sender, int serverPort, string serverIP);
        ICommunicator CreateCommunicator(int serverPort, string serverIP);
    }
}
