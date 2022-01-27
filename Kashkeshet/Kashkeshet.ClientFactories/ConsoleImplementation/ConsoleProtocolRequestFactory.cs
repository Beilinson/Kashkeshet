using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using Kashkeshet.ServerFactories.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.ConsoleImplementation
{
    public class ConsoleProtocolRequestFactory : IProtocolRequestFactory
    {
        public const string INVALID_FILE_ALERT = "Invalid File";
        public const string BLANK_MESSAGE = "";

        private readonly IInput _input;
        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;
        private readonly ChatFactory _chatCreator;

        public ConsoleProtocolRequestFactory(IInput input, IOutput output, IFileLoader fileLoader, ChatFactory chatCreator)
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

                { ChatProtocol.RequestChangeGroup, HandleChangeGroupRequest },
                { ChatProtocol.RequestLeaveGroup, HandleLeaveRequest },
                { ChatProtocol.RequestCreateGroup, HandleCreateGroupRequest },
                { ChatProtocol.RequestAddUser, HandleAddUserRequest }
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
                _output.Output(INVALID_FILE_ALERT);
            }
        }

        private void HandleLeaveRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), BLANK_MESSAGE, ChatProtocol.RequestLeaveGroup));
        }

        private void HandleChangeGroupRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), BLANK_MESSAGE, ChatProtocol.RequestAvailableGroups));
            var input = _input.Input();
            communicator.Send((communicator.ToString(), input, ChatProtocol.RequestChangeGroup));
        }

        private void HandleCreateGroupRequest(ICommunicator communicator)
        {
            var input = _input.Input();
            var newChat = _chatCreator.CreateBasicChat(input.ToString());
            communicator.Send((communicator.ToString(), newChat, ChatProtocol.RequestCreateGroup));
        }

        private void HandleAddUserRequest(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), BLANK_MESSAGE, ChatProtocol.RequestAllUsers));
            var input = _input.Input();
            communicator.Send((communicator.ToString(), input, ChatProtocol.RequestAddUser));
        }
    }
}
