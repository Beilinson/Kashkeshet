using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Kashkeshet.Common.Factories.Abstractions
{
    public interface ICommunicatorFactory
    {
        ICommunicator CreateCommunicator(Socket client, NetworkStream netStream);
    }
}
