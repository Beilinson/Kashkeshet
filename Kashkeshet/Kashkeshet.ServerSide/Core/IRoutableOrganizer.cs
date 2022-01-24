using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IRoutableOrganizer
    {
        void CreateRoute(IRoutable routable);
        void EnterRoute(IRoutable routable);
        IEnumerable<IRoutable> GetAvailableRoutes();
    }
}
