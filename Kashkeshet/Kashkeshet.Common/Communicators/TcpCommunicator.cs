using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Kashkeshet.Common.Communicators
{
    public class TcpCommunicator : ICommunicator
    {
        public TcpClient Client { get; private set; }

        private readonly NetworkStream _clientStream;
        private readonly IFormatter _formatter;

        public TcpCommunicator(TcpClient client, IFormatter formatter)
        {
            Client = client;
            _formatter = formatter;
            _clientStream = Client.GetStream();
        }

        public (object sender, object obj, ChatProtocol protocol) Receive()
        {
            var sender = _formatter.Deserialize(_clientStream);
            var obj = _formatter.Deserialize(_clientStream);
            var protocol = (ChatProtocol)_formatter.Deserialize(_clientStream);
            return (sender, obj, protocol);
        }

        public void Send(object sender, object obj, ChatProtocol protocol)
        {
            _formatter.Serialize(_clientStream, (sender, obj, protocol));
        }

        public override string ToString()
        {
            return "Client: " + Client.Client.RemoteEndPoint + " " + Client.Client.LocalEndPoint;
        }
    }
}
