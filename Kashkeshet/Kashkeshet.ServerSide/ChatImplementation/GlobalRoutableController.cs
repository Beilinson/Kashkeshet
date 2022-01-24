using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kashkeshet.ServerSide
{
    public class GlobalRoutableController : IRoutableOrganizer
    {
        public RoutableCollection Organizer { get; }

        private readonly IRoutable _globalRoute;

        public GlobalRoutableController(RoutableCollection organizer, IRoutable globalRoute)
        {
            Organizer = organizer;
            _globalRoute = globalRoute;
            
            // Global Chat:
            Organizer.UsersInRoutables.Add(_globalRoute, null);
        }

        public void AddUserToOrganizer(ICommunicator communicator)
        {
            if (Organizer.AllUsers.Add(communicator)) {
                Organizer.UsersInRoutables[_globalRoute].Add(communicator);
                Organizer.ActiveRoutable.Add(communicator, _globalRoute);
            }
        }

        public IEnumerable<ICommunicator> GetActiveUsers(IRoutable route)
        {
            return Organizer.UsersInRoutables[route]
                .Where(user => Organizer.ActiveRoutable[user] == route);
        }
    }
}
