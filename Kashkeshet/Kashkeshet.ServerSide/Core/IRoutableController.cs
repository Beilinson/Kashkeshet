using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using System.Collections.Generic;

namespace Kashkeshet.ServerSide.Core
{
    public interface IRoutableController
    {
        RoutableCollection Collection { get; }
        void AddUser(ICommunicator communicator);
        void RemoveUser(ICommunicator communicator);
        IEnumerable<UserData> GetActiveUsersInRoute(IRoutable route);
        IEnumerable<ICommunicator> GetActiveCommunicatorsInRoute(IRoutable route);
        bool FindRouteByName(string name, out IRoutable route);
    }
}
