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

        public (object sender, object obj) Receive()
        {
            object sender = _formatter.Deserialize(_clientStream);
            object obj = _formatter.Deserialize(_clientStream);
            return (sender, obj);
        }

        public void Send(object sender, object obj)
        {
            _formatter.Serialize(_clientStream, sender);
            _formatter.Serialize(_clientStream, obj);
        }

        public override string ToString()
        {
            return "Client: " + Client.Client.RemoteEndPoint + " " + Client.Client.LocalEndPoint;
        }
    }
}
