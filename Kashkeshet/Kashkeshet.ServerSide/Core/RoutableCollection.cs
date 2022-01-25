using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using System.Collections.Generic;

namespace Kashkeshet.ServerSide.Core
{
    public class RoutableCollection
    {
        public IDictionary<UserData, ICommunicator> UserMap { get; set; }
        public IDictionary<ICommunicator, UserData> AllUsers { get; set; }
        public IDictionary<UserData, IRoutable> ActiveRoutable { get; set; }
        public IDictionary<IRoutable, ICollection<UserData>> UsersInRoutables { get; set; }

        public RoutableCollection(
            IDictionary<UserData, ICommunicator> userMap,
            IDictionary<ICommunicator, UserData> allUsers, 
            IDictionary<UserData, IRoutable> activeRoutable, 
            IDictionary<IRoutable, ICollection<UserData>> usersInRoutables)
        {
            UserMap = userMap;
            AllUsers = allUsers;
            ActiveRoutable = activeRoutable;
            UsersInRoutables = usersInRoutables;
        }
    }
}
