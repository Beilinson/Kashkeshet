using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    public class ClientReceiver : IClientRunnable
    {
        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;

        public ClientReceiver(IOutput output)
        {
            _output = output;
        }

        public void Run(ICommunicator communicator)
        {
            try 
            {
                while (true)
                {
                    var received = communicator.Receive();
                    _output.Output(received);
                }
            }
            catch (Exception e)
            {
                _output.Output(e.Message);
            }
        }
    }
}
