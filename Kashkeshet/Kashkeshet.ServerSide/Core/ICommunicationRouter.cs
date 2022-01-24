using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface ICommunicationRouter
    {
        IRoutableOrganizer RoutableOrganizer { get; }
        void JoinClient();
        void ProcessCommunications();
    }
}
