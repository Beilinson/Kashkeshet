using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;

namespace Kashkeshet.ClientSide.ConsoleImplementation
{
    public class SimpleClientReceiver : IClientRunnable
    {
        private readonly IOutput _output;

        public SimpleClientReceiver(IOutput output)
        {
            _output = output;
        }

        public void Run(ICommunicator communicator)
        {
            try
            {
                while (true)
                {
                    var (sender, obj, protocol) = communicator.Receive();
                    _output.Output($"{sender} : {protocol} : {obj}");
                }
            }
            catch (Exception e)
            {
                _output.Output(e);
            }
        }

    }
}
