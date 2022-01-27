using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;

namespace Kashkeshet.ClientSide.ConsoleImplementation
{
    public class ClientResponseReceiver : IClientRunnable
    {
        private readonly IOutput _output;
        private readonly IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> _protocolOutputHandler;

        public ClientResponseReceiver(
            IOutput output,
            IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> protocolOutputHandler)
        {
            _output = output;
            _protocolOutputHandler = protocolOutputHandler;
        }

        public void Run(ICommunicator communicator)
        {
            try
            {
                while (true)
                {
                    var (sender, obj, protocol) = communicator.Receive();
                    
                    if (_protocolOutputHandler.TryGetValue(protocol, out var response))
                    {
                        response?.Invoke(sender, obj, protocol);
                    }
                    else
                    {
                        _output.Output($"{sender} : {protocol} : {obj}");
                    }
                }
            }
            catch (Exception e)
            {
                _output.Output(e);
            }
        }
    }
}
