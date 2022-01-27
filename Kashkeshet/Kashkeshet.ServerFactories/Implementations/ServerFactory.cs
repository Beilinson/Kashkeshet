using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.Factories.Implementations;
using Kashkeshet.Common.User;
using Kashkeshet.ServerFactories.Abstractions;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kashkeshet.ServerFactories.Implementations
{
    public class ServerFactory : IServerFactory
    {
        public const string GLOBAL_CHAT_NAME = "Global Chat";

        private readonly IChatFactory _chatFactory;
        private readonly IProtocolResponseFactory _protocolResponseFactory;

        public ServerFactory(IChatFactory chatFactory, IProtocolResponseFactory protocolResponseFactory)
        {
            _chatFactory = chatFactory;
            _protocolResponseFactory = protocolResponseFactory;
        }

        public IServer CreateServer()
        {
            var router = CreateServerRouter();

            var localPort = ServerAddress.PORT;
            var localIP = IPAddress.Parse(ServerAddress.IP_ADDRESS);

            var listener = new TcpListener(localIP, localPort);
            listener.Start();

            var server = new ChatServer(router, listener);

            return server;
        }

        public ICommunicationRouter CreateServerRouter()
        {
            var routeController = CreateRoutableController();
            var protocolHandler = _protocolResponseFactory.CreateResponseHandler();

            var formatter = new BinaryFormatter();
            var communicatorFactory = new TcpCommunicatorFactory(formatter);

            var router = new ChatRouter(routeController, protocolHandler, communicatorFactory);

            return router;
        }

        public IRoutableController CreateRoutableController()
        {
            var routeCollection = CreateRoutableCollection();
            var globalChat = _chatFactory.CreateBasicChat(GLOBAL_CHAT_NAME);
            var userDataFactory = new UserDataFactory();

            var routeController = new GlobalRoutableController(routeCollection, globalChat, userDataFactory);

            return routeController;
        }

        public RoutableCollection CreateRoutableCollection()
        {
            var userMap = new Dictionary<UserData, ICommunicator>();
            var allUsers = new Dictionary<ICommunicator, UserData>();
            var activeRoutable = new Dictionary<UserData, IRoutable>();
            var usersInRoutables = new Dictionary<IRoutable, ICollection<UserData>>();

            var routeCollection = new RoutableCollection(userMap, allUsers, activeRoutable, usersInRoutables);

            return routeCollection;
        }
    }
}
