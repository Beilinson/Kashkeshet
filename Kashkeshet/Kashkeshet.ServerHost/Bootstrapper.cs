using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Kashkeshet.ServerHost
{
    public class Bootstrapper
    {
        // Todo: Split into factories
        public IServer CreateChatServer()
        {
            var longHistory = new LongTermHistory(new List<object>());
            var globalChat = new Chat(longHistory);

            var routeCollection = new RoutableCollection();
            var routeController = new GlobalRoutableController(routeCollection, globalChat);

            BinaryFormatter formatter = new BinaryFormatter();
            var router = new ChatRouter(routeController, formatter);

            var localPort = 8080;
            var localIP = IPAddress.Parse("0.0.0.0");

            var listener = new TcpListener(localIP, localPort);
            listener.Start();

            var server = new ChatServer(router, listener);
            
            return server;
        }
    }
}
