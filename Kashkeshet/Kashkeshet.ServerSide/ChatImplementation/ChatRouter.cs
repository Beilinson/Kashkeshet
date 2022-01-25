using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading.Tasks;
using Kashkeshet.Common.User;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class ChatRouter : ICommunicationRouter
    {
        public const string USER_JOIN_STRING = "Has joined the Server!";
        public const string USER_LEAVE_STRING = "Has left the Server!";

        private readonly IServerController _controller;
        private readonly IFormatter _formatter;

        public ChatRouter(IServerController controller, IFormatter formatter)
        {
            _controller = controller;
            _formatter = formatter;
        }

        public void JoinClient(TcpClient client)
        {
            var communicator = new TcpCommunicator(client, _formatter);
            Task.Run(() =>
            {
                ProcessCommunications(communicator);
            });
        }

        private void ProcessCommunications(ICommunicator user)
        {
            try
            {
                _controller.RoutableController.AddUser(user);
                RevealHistory(user);

                var notifyJoin = (user.ToString(), USER_JOIN_STRING, ChatProtocol.Message);

                _controller.UserNotifyToActiveRoute(user, notifyJoin);

                while (true)
                {
                    var userData = user.Receive();
                    _controller.HandleProtocol(user, userData);
                }
            }
            catch
            {
                var notifyLeave = (user.ToString(), USER_LEAVE_STRING, ChatProtocol.Message);
                _controller.UserNotifyToActiveRoute(user, notifyLeave);
            }
        }

        private void RevealHistory(ICommunicator user)
        {
            var userData = _controller.RoutableController.Collection.AllUsers[user];
            var currentRoute = _controller.RoutableController.Collection.ActiveRoutable[userData];
            foreach (var data in currentRoute.MessageHistory.GetHistory())
            {
                user.Send(data);
            }
        }
    }
}
