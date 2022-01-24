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
        public const string USER_JOIN_STRING = "Has joined the channel!";
        public const string USER_LEAVE_STRING = "USER_LEAVE_STRING";

        public IRoutableOrganizer RoutableOrganizer { get; }
           
        private readonly IFormatter _formatter;

        public ChatRouter(IRoutableOrganizer routableOrganizer, IFormatter formatter)
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
                RoutableOrganizer.AddUserToOrganizer(user);
                UserNotifyToActiveRoute(user, $"{user.Client} {USER_JOIN_STRING}");

                while (true)
                {
                    var message = user.Receive();

                    UserNotifyToActiveRoute(user, message);
                }
            }
            catch
            {
                var message = $"{user.Client} {USER_LEAVE_STRING}";
                UserNotifyToActiveRoute(user, message);
                Console.WriteLine(message);
            }
        }



        private void UserNotifyToActiveRoute(ICommunicator user, object message)
        {
            // Active Route:
            IRoutable route = RoutableOrganizer.Organizer.ActiveRoutable[user];
            var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(route);

            // Redistributing message
            route.UpdateHistory(message);
            EchoMessage(message, activeUsers);
        }

        private void EchoMessage(object message, IEnumerable<ICommunicator> communicators)
        {
            Parallel.ForEach(communicators,
                communicator =>
                {
                    communicator.Send(message);
                });
        }
    }
}
