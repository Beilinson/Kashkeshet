using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kashkeshet.ClientHost
{
    public class ClientSender : IClientRunnable
    {
        private readonly IInput _input;
        private readonly IFileLoader _fileLoader;

        public ClientSender(IInput input, IFileLoader fileLoader)
        {
            _input = input;
            _fileLoader = fileLoader;
        }

        public void Run(ICommunicator communicator)
        {
            while (true)
            {
                var input = _input.Input();

                if (_fileLoader.TryLoadFile(input, out var file))
                {
                    communicator.Send((communicator.ToString(), file, ChatProtocol.File));
                }
                else
                {
                    communicator.Send((communicator.ToString(), input, ChatProtocol.Message));
                }
            }
        }
    }
}
