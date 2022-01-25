using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IServerController
    {
        IRoutableController RoutableController { get; }
        void HandleProtocol(ICommunicator user, (object sender, object message, ChatProtocol protocol) data);
        void UserNotifyToActiveRoute(ICommunicator user, (object, object, ChatProtocol) data);
    }
}
