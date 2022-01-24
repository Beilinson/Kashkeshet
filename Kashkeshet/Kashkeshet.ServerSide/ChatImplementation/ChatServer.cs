using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class ChatServer : IServer
    {
        public ICommunicationRouter CommunicationRouter { get; }

        private readonly TcpListener _listener;

        public ChatServer(ICommunicationRouter communicationRouter, TcpListener listener)
        {
            CommunicationRouter = communicationRouter;
            _listener = listener;
        }

        public void Run()
        {
            TcpClient client;

            while (true)
            {
                client = _listener.AcceptTcpClient();
                CommunicationRouter.JoinClient(client);
            }
        }
    }
}
