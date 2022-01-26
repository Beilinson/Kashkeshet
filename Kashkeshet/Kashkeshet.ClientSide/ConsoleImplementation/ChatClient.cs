using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.ClientSide.Implementations
{
    public class ChatClient : IClient
    {
        private readonly ICommunicator _communicator;
        private readonly IClientRunnable _clientReceiver;
        public readonly IClientRunnable _clientSender;

        public ChatClient(ICommunicator communicator, IClientRunnable clientReceiver, IClientRunnable clientSender)
        {
            _communicator = communicator;
            _clientReceiver = clientReceiver;
            _clientSender = clientSender;
        }

        public async Task Start()
        {
            var receiver = Task.Run(() =>
            {
                _clientReceiver.Run(_communicator);
            });
            var sender = Task.Run(() =>
            {
                _clientSender.Run(_communicator);
            });

            await Task.WhenAll(receiver, sender);
        }
    }
}
