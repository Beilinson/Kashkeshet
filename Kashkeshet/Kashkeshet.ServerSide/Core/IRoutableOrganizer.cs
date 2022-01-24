using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    // Todo: Might be better suited as DTO with public fields?
    public interface IRoutableOrganizer
    {
        void AddUserToOrganizer(ICommunicator communicator);
        void RemoveUserFromOrganizer(ICommunicator communicator);
        void AddUserToRoute(ICommunicator communicator);
        void CreateRoute(IRoutable routable);
        void EnterRoute(IRoutable routable);
        IEnumerable<IRoutable> GetAvailableRoutes();
    }
}
