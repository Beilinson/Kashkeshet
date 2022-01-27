using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;

namespace Kashkeshet.Common.Factories.Abstractions
{
    public interface IUserDataFactory
    {
        UserData CreateUser(ICommunicator communicator);
    }
}
