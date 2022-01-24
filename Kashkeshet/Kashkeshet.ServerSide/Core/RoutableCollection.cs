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
        public IDictionary<IRoutable, IList<ICommunicator>> UsersInRoutables { get; set; }
    }
}
