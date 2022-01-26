using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;

namespace Kashkeshet.ClientSide.Implementations
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
                var inputType = _input.Input().ToString();
                var inputProtocol = (ChatProtocol)Enum.Parse(typeof(ChatProtocol), inputType);

                if (_protocolRequestHandler.TryGetValue(inputProtocol, out var requestHandle))
                {
                    requestHandle?.Invoke(communicator);
                }
            }
        }
    }
}
