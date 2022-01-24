using System.Net.Sockets;

namespace PingPong.Common.Communicators
{
    public interface ICommunicator
    {
        TcpClient Client { get; }

        void Send(object obj);
        object Receive();
    }
}
