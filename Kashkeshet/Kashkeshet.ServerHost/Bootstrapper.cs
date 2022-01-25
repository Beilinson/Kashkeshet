using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerFactories;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kashkeshet.ServerHost
{
    public class Bootstrapper
    {
        // Todo: Split into factories
        public IServer CreateChatServer()
        {
            var longHistory = new LongTermHistory(new List<(object, object, ChatProtocol)>());
            var globalChat = new Chat(longHistory, "Global Chat");

            var userMap = new Dictionary<UserData, ICommunicator>();
            var allUsers = new Dictionary<ICommunicator, UserData>();
            var activeRoutable = new Dictionary<UserData, IRoutable>();
            var usersInRoutables = new Dictionary<IRoutable, ICollection<UserData>>();

            var routeCollection = new RoutableCollection(userMap, allUsers, activeRoutable, usersInRoutables);
            var routeController = new GlobalRoutableController(routeCollection, globalChat);
            var protocolResponseFactory = new ProtocolResponseFactory();
            var protocolHandler = protocolResponseFactory.CreateResponse();

            var responseController = new ChatResponseController(routeController, protocolHandler);

            var formatter = new BinaryFormatter();
            var router = new ChatRouter(responseController, formatter);

            var localPort = 8080;
            var localIP = IPAddress.Parse("127.0.0.1");

            var listener = new TcpListener(localIP, localPort);
            listener.Start();

            var server = new ChatServer(router, listener);
            
            return server;
        }
    }
}