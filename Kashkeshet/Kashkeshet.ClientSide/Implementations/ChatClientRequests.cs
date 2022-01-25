using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    public class ChatClientRequests : IClientRequests
    {
        private ICommunicator _communicator;
        private IOutput _output;
        private IInput _input;

        public void Run(ICommunicator communicator)
        {
            _communicator = communicator;
        }

        public void CreateGroupChat()
        {
            throw new NotImplementedException();
        }

        public void CreatePrivateChat()
        {
            throw new NotImplementedException();
        }

        public void EnterChat()
        {
            throw new NotImplementedException();
        }

        public void GetAvailableChats()
        {
            throw new NotImplementedException();
        }

        public void LeaveChat()
        {
            throw new NotImplementedException();
        }
    }
}
