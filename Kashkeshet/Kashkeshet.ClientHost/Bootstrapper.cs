using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientFactories.ConsoleImplementation;
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
            var output = new ConsoleOutput();

            var fileFactory = new GenericFileFactory();
            var fileLoader = new FileLoader(fileFactory);
            var chatFactory = new ChatFactory();

            var clientFactory = new ConsoleClientFactory(input, output);
            var protocolRequestFactory = new ConsoleProtocolRequestFactory(input, output, fileLoader, chatFactory);
            var protocolOutputFactory = new ConsoleProtocolOutputFactory(output, fileLoader);

            return InitiateClient(clientFactory, protocolRequestFactory, protocolOutputFactory);
        }

        private IClient InitiateClient(
            IClientFactory consoleClientFactory, 
            IProtocolRequestFactory protocolRequestFactory,
            IProtocolOutputFactory protocolOutputFactory)
        {
            var requestHandler = protocolRequestFactory.CreateProtocolRequestHandler();
            var sender = consoleClientFactory.CreateClientSender(requestHandler);
            
            var outputHandler = protocolOutputFactory.CreateProtocolOutputHandler();
            var receiver = consoleClientFactory.CreateClientReceiver(outputHandler);

            var client = consoleClientFactory.CreateChatClient(receiver, sender, ServerAddress.PORT, ServerAddress.IP_ADDRESS);

            return client;
        }
    }
}
