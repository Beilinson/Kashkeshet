using Kashkeshet.Common.Communicators;
using System.Net.Sockets;

namespace Kashkeshet.ServerSide.Core
{
    public interface ICommunicationRouter
    {
        void JoinClient(Socket client, NetworkStream netStream);
    }
}
