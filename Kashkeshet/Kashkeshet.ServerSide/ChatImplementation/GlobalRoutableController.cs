using Kashkeshet.Common.Communicators;
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
            Collection.UsersInRoutables.Add(_globalRoute, new List<ICommunicator>());
        }

        public void AddUser(ICommunicator communicator)
        {
            if (Collection.AllUsers.Add(communicator))
            {
                Collection.UsersInRoutables[_globalRoute].Add(communicator);
                Collection.ActiveRoutable.Add(communicator, _globalRoute);
            }
        }

        public void RemoveUser(ICommunicator communicator)
        {
            if (Collection.AllUsers.Remove(communicator))
            {
                Collection.UsersInRoutables[_globalRoute].Remove(communicator);
                Collection.ActiveRoutable.Remove(communicator);
            }
        }

        public IEnumerable<ICommunicator> GetActiveUsersInRoute(IRoutable route)
        {
            return Collection.UsersInRoutables[route]
                .Where(user => Collection.ActiveRoutable[user] == route);
        }
    }
}
