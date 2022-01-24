using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    // Todo: Might need to be redesigned with receiver and sender while loops in two separate classes
    public class ChatClient : IClient
    {
        public ICommunicator Communicator { get; }

        private bool _running;
        private readonly IOutput _output;

        public ChatClient(ICommunicator communicator, IOutput output)
        {
            Communicator = communicator;
            _output = output;
        }

        public void Start()
        {
            _running = true;
            while (_running)
            {
                var received = Communicator.Receive();
                _output.Output(received);
            }
        }

        public void Stop()
        {
            _running = false;
        }

        public void Send(object message)
        {
            Communicator.Send(Communicator.ToString(), message);
        }
    }
}
