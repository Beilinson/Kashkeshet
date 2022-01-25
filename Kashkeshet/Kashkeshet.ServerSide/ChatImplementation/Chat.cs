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

        public Chat(IMessageHistory messageHistory)
        {
            MessageHistory = messageHistory;
        }

        public void UpdateHistory((object sender, object message, ChatProtocol protocol) data)
        {
            MessageHistory.AddToHistory(data);
        }
    }
}
