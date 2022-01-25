using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Loaders;
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

        public ClientReceiver(IOutput output, IFileLoader fileLoader)
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
                    var (sender, obj) = communicator.Receive();
                    ParseOutput(sender, obj);
                }
            }
            catch (Exception e)
            {
                _output.Output(e.Message);
            }
        }

        private void ParseOutput(object sender, object message)
        {
            _output.Output($"{sender} : {message}");
            if (_fileLoader.IsFile(message, out FileObject file))
            {
                file.WriteFileToPath("C:/Code/Kashkeshet/ReceivedFile");
            }
        }
    }
}
