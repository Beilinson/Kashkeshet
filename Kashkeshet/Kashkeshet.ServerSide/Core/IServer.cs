using System;

namespace Kashkeshet.ServerSide.Core
{
    public interface IServer
    {
        ICommunicationRouter CommunicationRouter { get; }
        void Run();
    }
}
