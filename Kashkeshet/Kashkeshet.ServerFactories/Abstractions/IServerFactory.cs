using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerFactories.Abstractions
{
    public interface IServerFactory
    {
        RoutableCollection CreateRoutableCollection();
        IRoutableController CreateRoutableController();
        ICommunicationRouter CreateServerRouter();
        IServer CreateServer();
    }
}
