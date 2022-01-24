using System.Net.Sockets;

namespace Kashkeshet.Common.Communicators
{
    public interface ICommunicator
    {
        TcpClient Client { get; }

        void Send(object obj);
        object Receive();
    }
}
