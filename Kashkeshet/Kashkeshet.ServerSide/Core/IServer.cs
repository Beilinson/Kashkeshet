using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    public interface IServer
    {
        ICommunicationRouter CommunicationRouter { get; }
        void Run();
    }
}
