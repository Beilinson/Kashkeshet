using System.Net.Sockets;

namespace Kashkeshet.Common.Communicators
{
    public interface ICommunicator
    {
        Socket Client { get; }

        void Send((object senderID, object obj, ChatProtocol protocol) data);
        (object sender, object obj, ChatProtocol protocol) Receive();
    }
}