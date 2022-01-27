using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.User;

namespace Kashkeshet.Common.Factories.Implementations
{
    public class UserDataFactory : IUserDataFactory
    {
        public UserData CreateUser(ICommunicator communicator)
        {
            return new UserData(communicator.Client.RemoteEndPoint.ToString(), communicator.GetHashCode());
        }
    }
}
