using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class Chat : IRoutable
    {
        public IMessageHistory MessageHistory { get; }

        public Chat(IMessageHistory messageHistory)
        {
            MessageHistory = messageHistory;
        }

        public void UpdateHistory(object sender, object message)
        {
            MessageHistory.AddToHistory(sender, message);
        }
    }
}
