using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IRoutableController
    {
        RoutableCollection Collection { get; }
        void AddUser(ICommunicator communicator);
        void RemoveUser(ICommunicator communicator);
        IEnumerable<ICommunicator> GetActiveUsersInRoute(IRoutable route);
    }
}
