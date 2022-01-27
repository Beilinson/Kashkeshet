using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using System.Threading.Tasks;

namespace Kashkeshet.ClientSide.ConsoleImplementation
{
    public class ChatClient : IClient
    {
        private readonly ICommunicator _communicator;
        private readonly IClientRunnable _clientReceiver;
        public readonly IClientRunnable _clientSender;

        public ChatClient(ICommunicator communicator, IClientRunnable clientSender, IClientRunnable clientReceiver)
        {
            _communicator = communicator;
            _clientSender = clientSender;
            _clientReceiver = clientReceiver;
        }

        public async Task Start()
        {
            var sender = Task.Run(() =>
            {
                _clientSender.Run(_communicator);
            });
            var receiver = Task.Run(() =>
            {
                _clientReceiver.Run(_communicator);
            });

            await Task.WhenAll(receiver, sender);
        }
    }
}
