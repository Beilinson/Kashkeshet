using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientSide.Abstraction;

namespace Kashkeshet.ClientHost
{
    public class Bootstrapper
    {
        private readonly IClientFactory _consoleClientFactory;
        private readonly IProtocolRequestFactory _protocolRequestFactory;

        public Bootstrapper(IClientFactory consoleClientFactory, IProtocolRequestFactory protocolRequestFactory)
        {
            _consoleClientFactory = consoleClientFactory;
            _protocolRequestFactory = protocolRequestFactory;
        }

        public IClient CreateClient()
        {
            var receiver = _consoleClientFactory.CreateClientReceiver();

            var requestHandler = _protocolRequestFactory.CreateProtocolRequestHandler();
            var sender = _consoleClientFactory.CreateClientSender(requestHandler);

            var client = _consoleClientFactory.CreateChatClient(receiver, sender);

            return client;
        }
    }
}
