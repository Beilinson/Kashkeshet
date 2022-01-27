using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Kashkeshet.Common.User;
using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.Logging;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public delegate void ProtocolAction(
           IRoutableController controller,
           ICommunicator communicator,
           (object sender, object message, ChatProtocol protocol) data);

    public class ChatRouter : ICommunicationRouter
    {
        public const string USER_JOIN_STRING = "Has joined the Server!";
        public const string USER_LEAVE_STRING = "Has left the Server!";
        public const string BLANK_MESSAGE = "";

        private readonly IRoutableController _routableController;
        private readonly IDictionary<ChatProtocol, ProtocolAction> _protocolHandler;
        private readonly ICommunicatorFactory _communicatorFactory;

        public ChatRouter(
            IRoutableController routableController, 
            IDictionary<ChatProtocol, ProtocolAction> protocolHandler,
            ICommunicatorFactory communicatorFactory)
        {
            _routableController = routableController;
            _protocolHandler = protocolHandler;
            _communicatorFactory = communicatorFactory;
        }

        
        public void JoinClient(Socket client, NetworkStream netStream)
        {
            var communicator = _communicatorFactory.CreateCommunicator(client, netStream);

            Logger.Instance.Log.Info($"{communicator} has joined the Server");

            Task.Run(() =>
            {
                ProcessCommunications(communicator);
            });
        }

        private void ProcessCommunications(ICommunicator user)
        {
            try
            {
                HandleNewUser(user);

                while (true)
                {
                    var userData = user.Receive();
                    HandleProtocol(user, userData);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Log.Error(e);

                var notifyLeave = (user.ToString(), USER_LEAVE_STRING, ChatProtocol.Message);
                HandleProtocol(user, notifyLeave);
            }
        }

        private void HandleNewUser(ICommunicator user)
        {
            _routableController.AddUser(user);

            var userJoined = (user.ToString(), BLANK_MESSAGE, ChatProtocol.RequestHistory);
            HandleProtocol(user, userJoined);

            var notifyJoin = (user.ToString(), USER_JOIN_STRING, ChatProtocol.Message);
            HandleProtocol(user, notifyJoin);
        }

        private void HandleProtocol(ICommunicator user, (object sender, object message, ChatProtocol protocol) data)
        {
            if (_protocolHandler.TryGetValue(data.protocol, out var handler))
            {
                Logger.Instance.Log.Debug($"{user} - Request {data.protocol} is being handled");
                handler?.Invoke(_routableController, user, data);
            }
            else
            {
                user.Send(data);

                Logger.Instance.Log.Warn($"The protocol {data.protocol} isn't supported by this router's handler");
                Logger.Instance.Log.Warn($"Re-routing data back to the sender");
            }
        }
    }
}
