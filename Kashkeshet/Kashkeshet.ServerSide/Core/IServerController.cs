using Kashkeshet.Common.Communicators;

namespace Kashkeshet.ServerSide.Core
{
    public interface IServerController
    {
        IRoutableController RoutableController { get; }
        void HandleProtocol(ICommunicator user, (object sender, object message, ChatProtocol protocol) data);
    }
}
