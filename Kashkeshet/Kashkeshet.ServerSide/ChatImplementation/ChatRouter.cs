using Kashkeshet.Common.Communicators;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class ChatRouter : ICommunicationRouter
    {
        public const string USER_JOIN_STRING = "Has joined the Server!";
        public const string USER_LEAVE_STRING = "Has left the Server!";

        public IRoutableController RoutableOrganizer { get; }
           
        private readonly IFormatter _formatter;

        public ChatRouter(IRoutableController routableOrganizer, IFormatter formatter)
        {
            RoutableOrganizer = routableOrganizer;
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
                RoutableOrganizer.AddUser(user);
                RevealHistory(user);

                UserNotifyToActiveRoute(user, (user.ToString(), USER_JOIN_STRING, ChatProtocol.Message));

                while (true)
                {
                    var userData = user.Receive();

                    UserNotifyToActiveRoute(user, userData);
                }
            }
            catch
            {
                UserNotifyToActiveRoute(user, (user.ToString(), USER_LEAVE_STRING, ChatProtocol.Message));
            }
        }

        private void RevealHistory(ICommunicator user)
        {
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[user];
            foreach (var data in currentRoute.MessageHistory.GetHistory())
            {
                user.Send(data);
            }
        }

        private void UserNotifyToActiveRoute(ICommunicator user, (object, object, ChatProtocol) data)
        {
            // Active Route:
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[user];
            var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(currentRoute);

            if (!user.Client.Connected)
            {
                RoutableOrganizer.RemoveUser(user);
            }

            // Redistributing message
            currentRoute.UpdateHistory(data);
            EchoMessage(user, data, activeUsers);
        }

        private void EchoMessage(ICommunicator sender, (object, object, ChatProtocol) data, IEnumerable<ICommunicator> communicators)
        {
            Parallel.ForEach(communicators,
                communicator =>
                {
                    communicator.Send(data);
                });
        }
    }
}
