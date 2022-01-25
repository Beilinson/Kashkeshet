using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.Implementations;
using Kashkeshet.Common.Communicators;
using Kashkeshet.ConsoleUI;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kashkeshet.ClientHost
{
    public class Bootstrapper
    {
        public IClient CreateClient()
        {
            var output = new ConsoleOutput();

            var endPointPort = 8080;
            var endPointIP = IPAddress.Parse("127.0.0.1");

            var client = new TcpClient();
            client.Connect(endPointIP, endPointPort);

            var formatter = new BinaryFormatter();
            var communicator = new TcpCommunicator(client, formatter);

            var chatClient = new ChatClient(communicator, output);

            return chatClient;
        }

        public ConsoleClient CreateConsoleClient(IClient client)
        {
            var input = new ConsoleInput();
            var consoleClient = new ConsoleClient(client, input);

            return consoleClient;
        }
    }
}
