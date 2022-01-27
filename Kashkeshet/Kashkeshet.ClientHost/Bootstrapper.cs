using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientFactories.Implementations;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Factories.Implementations;
using Kashkeshet.Common.FileTypes;
using Kashkeshet.Common.Loaders;
using Kashkeshet.ConsoleUI;
using Kashkeshet.ServerFactories.Implementations;

namespace Kashkeshet.ClientHost
{
    public class Bootstrapper
    {
        public IClient CreateConsoleClient()
        {
            var input = new ConsoleInput();
            var ouput = new ConsoleOutput();

            var fileFactory = new GenericFileFactory();
            var fileLoader = new FileLoader(fileFactory);
            var chatFactory = new ChatFactory();

            var clientFactory = new ConsoleClientFactory(input, ouput, fileLoader);
            var protocolFactory = new ConsoleProtocolRequestFactory(input, ouput, fileLoader, chatFactory);

            return InitiateClient(clientFactory, protocolFactory);
        }

        private IClient InitiateClient(IClientFactory consoleClientFactory, IProtocolRequestFactory protocolRequestFactory)
        {
            var receiver = consoleClientFactory.CreateClientReceiver();

            var requestHandler = protocolRequestFactory.CreateProtocolRequestHandler();
            var sender = consoleClientFactory.CreateClientSender(requestHandler);

            var client = consoleClientFactory.CreateChatClient(receiver, sender, ServerAddress.PORT, ServerAddress.IP_ADDRESS);

            return client;
        }
    }
}
