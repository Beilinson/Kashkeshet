using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.FileTypes;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    public class SimpleClientReceiver : IClientRunnable
    {
        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;

        public SimpleClientReceiver(IOutput output, IFileLoader fileLoader)
        {
            _output = output;
            _fileLoader = fileLoader;
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
