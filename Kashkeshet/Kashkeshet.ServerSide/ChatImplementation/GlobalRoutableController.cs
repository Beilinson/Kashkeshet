using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.Core;
using System.Collections.Generic;
using System.Linq;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class GlobalRoutableController : IRoutableController
    {
        public RoutableCollection Collection { get; }

        private readonly IRoutable _globalRoute;
        private readonly IUserDataFactory _userDataFactory;

        public GlobalRoutableController(RoutableCollection collection, IRoutable globalRoute, IUserDataFactory userDataFactory)
        {
            Collection = collection;
            _globalRoute = globalRoute;
            _userDataFactory = userDataFactory;

            // Global Chat:
            Collection.UsersInRoutables.Add(_globalRoute, new List<UserData>());
        }

        public void AddUser(ICommunicator communicator)
        {
            var newUser = _userDataFactory.CreateUser(communicator);

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
                .Where(user => Collection.ActiveRoutable.TryGetValue(user, out var compareRoute) && compareRoute == route)
                .ToArray();
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

        public bool FindRouteByName(string name, out IRoutable route)
        {
            foreach (var availableRoute in Collection.UsersInRoutables.Keys)
            {
                if (availableRoute.Name == name)
                {
                    route = availableRoute;
                    return true;
                }
            }
            route = default;
            return false;
        }
    }
}
