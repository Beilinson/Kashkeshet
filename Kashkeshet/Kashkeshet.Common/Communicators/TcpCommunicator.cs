using System;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Kashkeshet.Common.Communicators
{
    public class TcpCommunicator : ICommunicator
    {
        public TcpClient Client { get; }

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

        public void Send((object senderID, object obj, ChatProtocol protocol) data)
        {
            _formatter.Serialize(_clientStream, data.senderID);
            _formatter.Serialize(_clientStream, data.obj);
            _formatter.Serialize(_clientStream, data.protocol);
        }

        public override string ToString()
        {
            return "Client: " + Client.Client.RemoteEndPoint + " " + Client.Client.LocalEndPoint;
        }
    }
}
