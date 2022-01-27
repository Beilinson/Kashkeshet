using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientFactories.ConsoleImplementation;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Factories.Implementations;
using Kashkeshet.Common.Loaders;
using Kashkeshet.Common.UI;
using Kashkeshet.ServerFactories.Implementations;

namespace Kashkeshet.ClientHost
{
    public class Bootstrapper
    {
        private readonly IInput _input;
        private readonly IOutput _output;

        public Bootstrapper(IInput input, IOutput output)
        {
            _input = input;
            _output = output;
        }

        public void ExplainProgram()
        {
            _output.Output("Welcome to Kashkeshet!");
            _output.Output("Before you connect, here is a short guideline on how to work this program: ");
            _output.Output("As a user, you must enter the protocol command you would like to send first.");
            _output.Output("For example, to send a message, you would first input : 'Message' and hit enter, and then write your desired message");
            _output.Output("The protocols are as follows: ");
            _output.Output("");
            _output.Output("Message             - To send a message");
            _output.Output("File                - To send a file");
            _output.Output("RequestLeaveGroup   - To leave the current group");
            _output.Output("RequestChangeGroup  - To change your current group with one of the available groups");
            _output.Output("RequestCreateGroup  - To create a new group with a name of your choice");
            _output.Output("RequestAddUser      - To add a user to your current group by his ID number");
            _output.Output("");
        }

        public IClient CreateConsoleClient()
        {
            var fileFactory = new GenericFileFactory();
            var fileLoader = new FileLoader(fileFactory);
            var chatFactory = new ChatFactory();

            var clientFactory = new ConsoleClientFactory(_input, _output);
            var protocolRequestFactory = new ConsoleProtocolRequestFactory(_input, _output, fileLoader, chatFactory);
            var protocolOutputFactory = new ConsoleProtocolOutputFactory(_output, fileLoader);

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
