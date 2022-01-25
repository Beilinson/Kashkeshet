using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using Kashkeshet.Common.User;
using Kashkeshet.ServerFactories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    public class ClientRequestSender : IClientRunnable
    {
        private readonly IInput _input;
        private readonly IFileLoader _fileLoader;
        private readonly ChatCreator _chatCreator;

        public ClientRequestSender(IInput input, IFileLoader fileLoader, ChatCreator chatCreator)
        {
            _input = input;
            _fileLoader = fileLoader;
            _chatCreator = chatCreator;
        }

        public void Run(ICommunicator communicator)
        {
            while (true)
            {
                var inputType = _input.Input();
                var input = _input.Input();
                ChatProtocol myEnum = (ChatProtocol)Enum.Parse(typeof(ChatProtocol), inputType.ToString());

                switch (myEnum)
                {
                    case ChatProtocol.Message:
                        communicator.Send((communicator.ToString(), input, ChatProtocol.Message));
                        break;
                    case ChatProtocol.File:
                        if (_fileLoader.TryLoadFile(input, out var file))
                        {
                            communicator.Send((communicator.ToString(), file, ChatProtocol.File));
                        }
                        break;
                    case ChatProtocol.LeaveGroup:
                        communicator.Send((communicator.ToString(), input, ChatProtocol.LeaveGroup));
                        break;
                    case ChatProtocol.ChangeGroup:
                        communicator.Send((communicator.ToString(), input, ChatProtocol.GetAvailableGroups));
                        input = _input.Input();
                        communicator.Send((communicator.ToString(), input, ChatProtocol.ChangeGroup));
                        break;
                    case ChatProtocol.CreateGroup:
                        communicator.Send((communicator.ToString(), _chatCreator.CreateBasicChat(input.ToString()), ChatProtocol.CreateGroup));
                        break;
                    default:
                        communicator.Send((communicator.ToString(), input, myEnum));
                        break;
                }
            }
        }
    }
}
