using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerFactories.Abstractions;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System.Collections.Generic;

namespace Kashkeshet.ServerFactories.Implementations
{
    public class ChatFactory : IChatFactory
    {
        public IRoutable CreateBasicChat(string name)
        {
            var longHistory = new LongTermHistory(new List<(object, object, ChatProtocol)>());
            var chat = new Chat(longHistory, name);

            return chat;
        }
    }
}
