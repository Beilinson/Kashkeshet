using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public delegate void ProtocolAction(
        IRoutableController controller, 
        ICommunicator communicator, 
        (object sender, object message, ChatProtocol protocol) data);

    public class ChatResponseController : IServerController
    {
        public IRoutableController RoutableController { get; }

        private readonly IDictionary<ChatProtocol, ProtocolAction> _protocolHandler;

        public ChatResponseController(IRoutableController routableController, IDictionary<ChatProtocol, ProtocolAction> protocolHandler)
        {
            RoutableController = routableController;
            _protocolHandler = protocolHandler;
        }

        public void HandleProtocol(ICommunicator user, (object sender, object message, ChatProtocol protocol) data)
        {
            if (_protocolHandler.TryGetValue(data.protocol, out var handler))
            {
                handler?.Invoke(RoutableController, user, data);
            } 
            else
            {
                user.Send(data);
            }
        }
    }
}
