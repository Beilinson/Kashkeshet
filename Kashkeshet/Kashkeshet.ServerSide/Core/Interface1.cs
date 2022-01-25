using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IServerController
    {
        IRoutableController RoutableController { get; }
        (object sender, object message, ChatProtocol protocol) HandleProtocol((object sender, object message, ChatProtocol protocol) data);
        void UserNotifyToActiveRoute(ICommunicator user, (object, object, ChatProtocol) data);
    }
}
