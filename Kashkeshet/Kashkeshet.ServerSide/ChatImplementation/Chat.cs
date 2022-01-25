using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    [Serializable]
    public class Chat : IRoutable
    {
        public IMessageHistory MessageHistory { get; }
        public string Name { get; }

        public Chat(IMessageHistory messageHistory, string name)
        {
            MessageHistory = messageHistory;
            Name = name;
        }

        public void UpdateHistory((object sender, object message, ChatProtocol protocol) data)
        {
            MessageHistory.AddToHistory(data);
        }
    }
}
