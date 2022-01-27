using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.Implementations;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using Kashkeshet.ServerFactories.Implementations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kashkeshet.ClientFactories.Implementations
{
    public class ConsoleClientFactory : IClientFactory
    {
        private readonly IInput _input;
        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;

        public ConsoleClientFactory(IInput input, IOutput output, IFileLoader fileLoader)
        {
            _input = input;
            _output = output;
            _fileLoader = fileLoader;
        }

        public IClientRunnable CreateClientReceiver()
        {
            var clientReceiver = new SimpleClientReceiver(_output, _fileLoader);

            return clientReceiver;
        }

        public IClientRunnable CreateClientSender(IDictionary<ChatProtocol, Action<ICommunicator>> protocolHandler)
        {
            var clientSender = new ClientRequestSender(_input, protocolHandler);

            return clientSender;
        }

        public IClient CreateChatClient(IClientRunnable receiver, IClientRunnable sender, int serverPort, string serverIP)
        {
            var communicator = CreateCommunicator(serverPort, serverIP);

            var chatClient = new ChatClient(communicator, receiver, sender);

            return chatClient;
        }

        public ICommunicator CreateCommunicator(int serverPort, string serverIP)
        {
            var endPointPort = serverPort;
            var endPointIP = IPAddress.Parse(serverIP);

            var client = new TcpClient();
            client.Connect(endPointIP, endPointPort);

            var formatter = new BinaryFormatter();
            var communicator = new TcpCommunicator(client, formatter);

            return communicator;
        }
    }
}
