using System.Net.Sockets;

namespace Kashkeshet.ServerSide.Core
{
    public interface ICommunicationRouter
    {
        void JoinClient(TcpClient client);
    }
}
