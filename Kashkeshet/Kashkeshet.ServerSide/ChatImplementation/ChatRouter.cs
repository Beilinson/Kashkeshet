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
                UserNotifyToActiveRoute(user, USER_JOIN_STRING);

                while (true)
                {
                    var message = user.Receive();

                    UserNotifyToActiveRoute(user, message);
                }
            }
            catch
            {
                UserNotifyToActiveRoute(user, USER_LEAVE_STRING);
            }
        }

        private void UserNotifyToActiveRoute(ICommunicator user, object message)
        {
            // Active Route:
            IRoutable route = RoutableOrganizer.Collection.ActiveRoutable[user];
            var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(route);

            if (!user.Client.Connected)
            {
                RoutableOrganizer.RemoveUser(user);
            }

            // Redistributing message
            route.UpdateHistory(message);
            EchoMessage(user, message, activeUsers);
        }

        private void EchoMessage(ICommunicator sender, object message, IEnumerable<ICommunicator> communicators)
        {
            Parallel.ForEach(communicators,
                communicator =>
                {
                    // This isn't very right, but ignore for now because it makes the console work nicely.
                    communicator.Send(sender.ToString());
                    communicator.Send(' ');
                    communicator.Send(message);
                    communicator.Send('\n');
                });
        }
    }
}
