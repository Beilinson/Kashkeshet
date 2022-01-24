using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public class RoutableCollection
    {
        public HashSet<ICommunicator> AllUsers { get; set; }
        public IDictionary<ICommunicator, IRoutable> ActiveRoutable { get; set; }
        public IDictionary<IRoutable, ICollection<ICommunicator>> UsersInRoutables { get; set; }

        public RoutableCollection(
            HashSet<ICommunicator> allUsers, 
            IDictionary<ICommunicator, IRoutable> activeRoutable, 
            IDictionary<IRoutable, ICollection<ICommunicator>> usersInRoutables)
        {
            AllUsers = allUsers;
            ActiveRoutable = activeRoutable;
            UsersInRoutables = usersInRoutables;
        }
    }
}
