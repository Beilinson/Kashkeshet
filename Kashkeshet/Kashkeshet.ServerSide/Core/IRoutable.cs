using Kashkeshet.Common.Communicators;

namespace Kashkeshet.ServerSide.Core
{
    public interface IRoutable
    {
        IMessageHistory MessageHistory { get; }
        string Name { get; }
        void UpdateHistory((object sender, object message, ChatProtocol protocol) data);
    }
}
