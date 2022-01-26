using Kashkeshet.Common.Communicators;
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

            var localPort = 8080;
            var localIP = IPAddress.Parse("127.0.0.1");

            var listener = new TcpListener(localIP, localPort);
            listener.Start();

            var server = new ChatServer(router, listener);

            return server;
        }

        public ICommunicationRouter CreateServerRouter()
        {
            var routeController = CreateRoutableController();
            var protocolHandler = _protocolResponseFactory.CreateResponseHandler();

            var responseController = new ChatResponseController(routeController, protocolHandler);

            var formatter = new BinaryFormatter();
            var router = new ChatRouter(responseController, formatter);

            return router;
        }

        public IRoutableController CreateRoutableController()
        {
            var routeCollection = CreateRoutableCollection();
            var globalChat = _chatFactory.CreateBasicChat("Global Chat");

            var routeController = new GlobalRoutableController(routeCollection, globalChat);

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
