using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;

namespace Kashkeshet.ServerFactories
{
    public class ChatCreator
    {
        public IRoutable CreateBasicChat(string name)
        {
            var longHistory = new LongTermHistory(new List<(object, object, ChatProtocol)>());
            var chat = new Chat(longHistory, name);

            return chat;
        }
    }
}
