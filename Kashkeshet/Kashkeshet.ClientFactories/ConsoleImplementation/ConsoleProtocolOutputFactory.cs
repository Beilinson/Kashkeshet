using Kashkeshet.ClientFactories.Abstractions;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientFactories.ConsoleImplementation
{
    public class ConsoleProtocolOutputFactory : IProtocolOutputFactory
    {
        public const string FILE_DIR = "C:/Code/ReceivedFiles/";
        public const string RECEIVED_VALID_FILE = "Received a valid file:";
        public const string RECEIVED_INVALID_FILE = "Is an invalid file!";

        private readonly IOutput _output;
        private readonly IFileLoader _fileLoader;

        public ConsoleProtocolOutputFactory(IOutput output, IFileLoader fileLoader)
        {
            _output = output;
            _fileLoader = fileLoader;
        }

        public IDictionary<ChatProtocol, Action<object, object, ChatProtocol>> CreateProtocolOutputHandler()
        {
            return new Dictionary<ChatProtocol, Action<object, object, ChatProtocol>> 
            {
                { ChatProtocol.Message, HandleBasicOutput },
                { ChatProtocol.File, HandleFileOutput},
                { ChatProtocol.Audio, HandleFileOutput},

                { ChatProtocol.RequestAvailableGroups, HandleBasicOutput },
                { ChatProtocol.RequestAddUser, HandleBasicOutput },
                { ChatProtocol.RequestLeaveGroup, HandleBasicOutput },
                { ChatProtocol.RequestAllUsers, HandleBasicOutput },
                { ChatProtocol.RequestCreateGroup, HandleBasicOutput },
                { ChatProtocol.RequestChangeGroup, HandleBasicOutput }
            };
        }

        private void HandleBasicOutput(object sender, object message, ChatProtocol protocol)
        {
            if (message.GetType().IsArray)
            {
                foreach (var element in message as Array)
                {
                    _output.Output($"{sender} : {protocol} : {element}");
                }
            }
            else
            {
                _output.Output($"{sender} : {protocol} : {message}");
            }
        }

        private void HandleFileOutput(object sender, object message, ChatProtocol protocol)
        {
            if (_fileLoader.IsFile(message, out var file))
            {
                file.WriteFileToPath($"{FILE_DIR}{sender.ToString().GetHashCode()}", file.Name);
                _output.Output($"{sender} : {protocol} : {RECEIVED_VALID_FILE} {message}");
            }
            else
            {
                _output.Output($"{sender} : {protocol} : {message} {RECEIVED_INVALID_FILE}");
            }
        }
    }
}
