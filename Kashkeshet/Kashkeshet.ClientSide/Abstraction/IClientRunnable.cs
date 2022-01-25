using Kashkeshet.Common.Communicators;

namespace Kashkeshet.ClientSide.Abstraction
{
    public interface IClientRunnable
    {
        void Run(ICommunicator communicator);
    }
}
