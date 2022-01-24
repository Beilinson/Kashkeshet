using System.Net.Sockets;
using System.Runtime.Serialization;

namespace PingPong.Common.Communicators
{
    public class TcpCommunicator : ICommunicator
    {
        public TcpClient Client{ get; private set; }

        private NetworkStream _clientStream;
        private IFormatter _formatter;

        public TcpCommunicator(TcpClient client, IFormatter formatter)
        {
            Client = client;
            _formatter = formatter;
            _clientStream = Client.GetStream();
        }

        public object Receive()
        {
            object obj = _formatter.Deserialize(_clientStream);
            return obj;
        }

        public void Send(object obj)
        {
            _formatter.Serialize(_clientStream, obj);
        }
    }
}
