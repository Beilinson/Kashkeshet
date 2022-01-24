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

                UserNotifyToActiveRoute(user, USER_JOIN_STRING);

                while (true)
                {
                    var (sender, message) = user.Receive();

                    UserNotifyToActiveRoute(user, message);
                }
            }
            catch
            {
                UserNotifyToActiveRoute(user, USER_LEAVE_STRING);
            }
        }

        private void RevealHistory(ICommunicator user)
        {
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[user];
            foreach (var (sender, message) in currentRoute.MessageHistory.GetHistory())
            {
                user.Send(sender, message);
            }
        }

        private void UserNotifyToActiveRoute(ICommunicator user, object message)
        {
            // Active Route:
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[user];
            var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(currentRoute);

            if (!user.Client.Connected)
            {
                RoutableOrganizer.RemoveUser(user);
            }

            // Redistributing message
            currentRoute.UpdateHistory(user.ToString(), message);
            EchoMessage(user, message, activeUsers);
        }

        private void EchoMessage(ICommunicator sender, object message, IEnumerable<ICommunicator> communicators)
        {
            Parallel.ForEach(communicators,
                communicator =>
                {
                    communicator.Send(sender.ToString(), message);
                });
        }
    }
}
