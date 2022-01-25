using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.Implementations;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Loaders;
using Kashkeshet.ConsoleUI;
using Kashkeshet.ServerFactories;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kashkeshet.ClientHost
{
    public class Bootstrapper
    {
        public IClientRunnable CreateClientReceiver()
        {
            var output = new ConsoleOutput();
            var fileLoader = new FileLoader();

            var clientReceiver = new ClientReceiver(output, fileLoader);

            return clientReceiver;
        }

        public IClientRunnable CreateClientSender()
        {
            var input = new ConsoleInput();
            var fileLoader = new FileLoader();
            var chatCreator = new ChatCreator();

            var clientSender = new SimpleClientSender(input);

            return clientSender;
        }

        public IClientRunnable CreateComplexClientSender()
        {
            var input = new ConsoleInput();
            var fileLoader = new FileLoader();
            var chatCreator = new ChatCreator();

            var clientSender = new ClientRequestSender(input, fileLoader, chatCreator);

            return clientSender;
        }

        public IClient CreateChatClient(IClientRunnable receiver, IClientRunnable sender)
        {
            var endPointPort = 8080;
            var endPointIP = IPAddress.Parse("127.0.0.1");

            var client = new TcpClient();
            client.Connect(endPointIP, endPointPort);

            var formatter = new BinaryFormatter();
            var communicator = new TcpCommunicator(client, formatter);

            var chatClient = new ChatClient(communicator, receiver, sender);

            return chatClient;
        }
    }
}
