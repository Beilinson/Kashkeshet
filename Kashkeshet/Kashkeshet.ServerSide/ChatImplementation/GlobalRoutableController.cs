using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class GlobalRoutableController : IRoutableController
    {
        public RoutableCollection Collection { get; }

        private readonly IRoutable _globalRoute;

        public GlobalRoutableController(RoutableCollection collection, IRoutable globalRoute)
        {
            Collection = collection;
            _globalRoute = globalRoute;

            // Global Chat:
            Collection.UsersInRoutables.Add(_globalRoute, new List<UserData>());
        }

        public void AddUser(ICommunicator communicator)
        {
            var newUser = new UserData(communicator.Client.Client.RemoteEndPoint.ToString(), communicator.GetHashCode());
            Collection.UserMap.Add(newUser, communicator);
            Collection.AllUsers.Add(communicator, newUser);
            Collection.UsersInRoutables[_globalRoute].Add(newUser);
            Collection.ActiveRoutable.Add(newUser, _globalRoute);
        }

        public void RemoveUser(ICommunicator communicator)
        {
            var userData = Collection.AllUsers[communicator];
            if (Collection.AllUsers.Remove(communicator))
            {
                Collection.UsersInRoutables[_globalRoute].Remove(userData);
                Collection.ActiveRoutable.Remove(userData);
            }
        }

        public IEnumerable<UserData> GetActiveUsersInRoute(IRoutable route)
        {
            return Collection.UsersInRoutables[route]
                .Where(user => Collection.ActiveRoutable[user] == route);
        }

        public IEnumerable<ICommunicator> GetActiveCommunicatorsInRoute(IRoutable route)
        {
            var activeUsers = GetActiveUsersInRoute(route);

            var activeCommunicators = new List<ICommunicator>();
            foreach (var active in activeUsers)
            {
                activeCommunicators.Add(Collection.UserMap[active]);
            }

            return activeCommunicators;
        }
    }
}
