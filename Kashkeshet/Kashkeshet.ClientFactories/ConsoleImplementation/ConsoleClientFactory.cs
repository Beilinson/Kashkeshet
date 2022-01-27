using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.ConsoleImplementation;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using Kashkeshet.ServerFactories.Implementations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kashkeshet.ClientFactories.ConsoleImplementation
{
    public class ConsoleClientFactory : IClientFactory
    {
        private readonly IInput _input;
        private readonly IOutput _output;

        public ConsoleClientFactory(IInput input, IOutput output)
        {
            _input = input;
            _output = output;
        }

        public IClientRunnable CreateClientReceiver(IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> protocolOutputHandler)
        {
            var clientReceiver = new ClientResponseReceiver(_output, protocolOutputHandler);

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
            var communicator = new TcpCommunicator(client.Client, client.GetStream(), formatter);

            return communicator;
        }
    }
}
