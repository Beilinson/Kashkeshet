using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;

namespace Kashkeshet.ClientSide.ConsoleImplementation
{
    public class ClientRequestSender : IClientRunnable
    {
        private readonly IInput _input;
        private readonly IDictionary<ChatProtocol, Action<ICommunicator>> _protocolRequestHandler;

        public ClientRequestSender(IInput input, IDictionary<ChatProtocol, Action<ICommunicator>> protocolRequestHandler)
        {
            _input = input;
            _protocolRequestHandler = protocolRequestHandler;
        }

        public void Run(ICommunicator communicator)
        {
            while (true)
            {
                var input = _input.Input().ToString();

                if (Enum.TryParse(typeof(ChatProtocol), input, out var protocol))
                {
                    if (_protocolRequestHandler.TryGetValue((ChatProtocol)protocol, out var requestHandle))
                    {
                        requestHandle?.Invoke(communicator);
                    }
                }
            }
        }
    }
}
