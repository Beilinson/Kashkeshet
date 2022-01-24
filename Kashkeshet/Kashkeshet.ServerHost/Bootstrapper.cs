using Kashkeshet.Common.Communicators;
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
            var longHistory = new LongTermHistory(new List<(object, object)>());
            var globalChat = new Chat(longHistory);

            var allUsers = new HashSet<ICommunicator>();
            var activeRoutable = new Dictionary<ICommunicator, IRoutable>();
            var usersInRoutables = new Dictionary<IRoutable, ICollection<ICommunicator>>();

            var routeCollection = new RoutableCollection(allUsers, activeRoutable, usersInRoutables);
            var routeController = new GlobalRoutableController(routeCollection, globalChat);

            var formatter = new BinaryFormatter();
            var router = new ChatRouter(routeController, formatter);

            var localPort = 8080;
            var localIP = IPAddress.Parse("127.0.0.1");

            var listener = new TcpListener(localIP, localPort);
            listener.Start();

            var server = new ChatServer(router, listener);
            
            return server;
        }
    }
}