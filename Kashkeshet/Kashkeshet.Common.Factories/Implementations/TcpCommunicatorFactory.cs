using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Factories.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;

namespace Kashkeshet.Common.Factories.Implementations
{
    public class TcpCommunicatorFactory : ICommunicatorFactory
    {
        private readonly IFormatter _formatter;

        public ICommunicator CreateCommunicator(Socket client, NetworkStream netStream)
        {
            return new TcpCommunicator(client, netStream, _formatter);
        }
    }
}
