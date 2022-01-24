using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    // Todo: Might be better suited as DTO with public fields?
    public interface IRoutableOrganizer
    {
        RoutableCollection Organizer { get; }
        void AddUserToOrganizer(ICommunicator communicator);
        IEnumerable<ICommunicator> GetActiveUsers(IRoutable route);
    }
}
