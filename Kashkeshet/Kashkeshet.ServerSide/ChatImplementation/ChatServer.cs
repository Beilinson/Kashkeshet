using Kashkeshet.Common.Logging;
using Kashkeshet.ServerSide.Core;
using System.Net.Sockets;

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
            Logger.Instance.Log.Info("The server is running");
            while (true)
            {
                var client = _listener.AcceptTcpClient();
                CommunicationRouter.JoinClient(client.Client, client.GetStream());
            }
        }
    }
}
