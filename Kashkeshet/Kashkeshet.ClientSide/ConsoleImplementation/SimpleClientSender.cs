using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;

namespace Kashkeshet.ClientSide.ConsoleImplementation
{
    public class SimpleClientSender : IClientRunnable
    {
        private readonly IInput _input;

        public SimpleClientSender(IInput input)
        {
            _input = input;
        }

        public void Run(ICommunicator communicator)
        {
            while (true)
            {
                var input = _input.Input();

                communicator.Send((communicator.ToString(), input, ChatProtocol.Message));
            }
        }
    }
}
