using Kashkeshet.Common.Communicators;

namespace Kashkeshet.ServerSide.Core
{
    // Exists because it might have a bigger role in the future
    public interface IRoutable
    {
        IMessageHistory MessageHistory { get; }
        void UpdateHistory((object sender, object message, ChatProtocol protocol) data);
    }
}
