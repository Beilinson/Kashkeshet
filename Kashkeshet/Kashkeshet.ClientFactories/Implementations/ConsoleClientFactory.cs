using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.Implementations;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
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
            var clientReceiver = new ClientReceiver(_output, _fileLoader);

            return clientReceiver;
        }

        public IClientRunnable CreateClientSender(IDictionary<ChatProtocol, Action<ICommunicator>> protocolHandler)
        {
            var clientSender = new ClientRequestSender(_input, protocolHandler);

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
