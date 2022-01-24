using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface ICommunicationRouter
    {
        void JoinClient(TcpClient client);
        //void ProcessCommunications(ICommunicator communicator);
    }
}
