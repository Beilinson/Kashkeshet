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
                    var (sender, obj, protocol) = communicator.Receive();
                    ParseOutput(sender, obj, protocol);
                }
            }
            catch (Exception e)
            {
                _output.Output(e);
            }
        }

        private void ParseOutput(object sender, object message, ChatProtocol protocol)
        {
            _output.Output($"{sender} : {protocol} : {message}");
            if (_fileLoader.IsFile(message, out GenericFile file))
            {
                file.WriteFileToPath($"C:/Code/ReceivedFiles/{sender.ToString().GetHashCode()}", "ReceivedFile");
            } 
            else if (protocol == ChatProtocol.DataRequest)
            {
                PrintRandomData(message);
            }
        }

        private void PrintRandomData(object data)
        {
            Console.WriteLine($"{data} WHOAAAAAAA {data.GetHashCode()}");
        }
    }
}
