using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using Kashkeshet.ServerFactories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.Implementations
{
    public class ProtocolRequestFactory : IProtocolRequestFactory
    {
        private readonly IInput _input;
        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;
        private readonly ChatFactory _chatCreator;

        public ProtocolRequestFactory(IInput input, IOutput output, IFileLoader fileLoader, ChatFactory chatCreator)
        {
            _input = input;
            _output = output;
            _fileLoader = fileLoader;
            _chatCreator = chatCreator;
        }

        public IDictionary<ChatProtocol, Action<ICommunicator>> CreateProtocolRequestHandler()
        {
            return new Dictionary<ChatProtocol, Action<ICommunicator>>
            {
                { ChatProtocol.Message, HandleMessageRequest },
                { ChatProtocol.File, HandleFileRequest },
                { ChatProtocol.ChangeGroup, HandleChangeGroupRequest },
                { ChatProtocol.LeaveGroup, HandleLeaveRequest },
                { ChatProtocol.CreateGroup, HandleCreateGroupRequest },
                { ChatProtocol.AddUser, HandleAddUserRequest }
            };
        }

        private void HandleMessageRequest(ICommunicator communicator)
        {
            var input = _input.Input();
            communicator.Send((communicator.ToString(), input, ChatProtocol.Message));
        }

        private void HandleFileRequest(ICommunicator communicator)
        {
            var input = _input.Input();
            if (_fileLoader.TryLoadFile(input, out var file))
            {
                communicator.Send((communicator.ToString(), file, ChatProtocol.File));
            }
            else
            {
                _output.Output("Invalid file");
            }
        }

        private void HandleLeaveRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), "", ChatProtocol.LeaveGroup));
        }

        private void HandleChangeGroupRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), "", ChatProtocol.GetAvailableGroups));
            var input = _input.Input();
            communicator.Send((communicator.ToString(), input, ChatProtocol.ChangeGroup));
        }

        private void HandleCreateGroupRequest(ICommunicator communicator)
        {
            var input = _input.Input();
            var newChat = _chatCreator.CreateBasicChat(input.ToString());
            communicator.Send((communicator.ToString(), newChat, ChatProtocol.CreateGroup));
        }

        private void HandleAddUserRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), "", ChatProtocol.RequestUsers));
            var input = _input.Input();
            communicator.Send((communicator.ToString(), input, ChatProtocol.AddUser));
        }
    }
}
