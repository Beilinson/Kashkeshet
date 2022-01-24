using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Implementations
{
    public class ChatClient : IClient
    {
        public ICommunicator Communicator { get; }

        private bool _running;
        private readonly IInput _input;
        private readonly IOutput _output;
        public ChatClient(ICommunicator communicator, IInput input, IOutput output)
        {
            Communicator = communicator;
            _input = input;
            _output = output;
        }

        public void Start()
        {
            _running = true;
            while (_running)
            {
                var received = Communicator.Receive();
                _output.Output(received);
            }
        }

        public void Stop()
        {
            _running = false;
        }

        public void SendMessage(string message)
        {
            Communicator.Send(message);
        }

        public void SendFile(object file)
        {
            Communicator.Send(file);
        }

        public void SendAudio(object audio)
        {
            Communicator.Send(audio);
        }
    }
}
