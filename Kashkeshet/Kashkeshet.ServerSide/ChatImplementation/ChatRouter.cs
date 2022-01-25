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

                    var data = CheckIsExecutable(userData);

                    UserNotifyToActiveRoute(user, data);
                }
            }
            catch
            {
                UserNotifyToActiveRoute(user, (user.ToString(), USER_LEAVE_STRING, ChatProtocol.Message));
            }
        }

        private void RevealHistory(ICommunicator user)
        {
            var userData = RoutableOrganizer.Collection.AllUsers[user];
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[userData];
            foreach (var data in currentRoute.MessageHistory.GetHistory())
            {
                user.Send(data);
            }
        }

        private void UserNotifyToActiveRoute(ICommunicator user, (object, object, ChatProtocol) data)
        {
            // Active Route:
            var userData = RoutableOrganizer.Collection.AllUsers[user];
            var currentRoute = RoutableOrganizer.Collection.ActiveRoutable[userData];

            if (!user.Client.Connected)
            {
                RoutableOrganizer.RemoveUser(user);
            }

            var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(currentRoute);
            var activeCommunicators = new List<ICommunicator>();
            foreach (var active in activeUsers)
            {
                activeCommunicators.Add(RoutableOrganizer.Collection.UserMap[active]);
            }
            // Redistributing message
            currentRoute.UpdateHistory(data);
            EchoMessage(data, activeCommunicators);
        }

        private void EchoMessage((object, object, ChatProtocol) data, IEnumerable<ICommunicator> communicators)
        {
            Parallel.ForEach(communicators,
                communicator =>
                {
                    communicator.Send(data);
                });
        }

        private (object, object, ChatProtocol) CheckIsExecutable((object sender, object possibleExecutable, ChatProtocol protocol) data)
        {
            var dat = data.possibleExecutable;
            if (data.protocol.Equals(ChatProtocol.DataRequest))
            {
                dat = new UserData("Hello", 213);//RoutableOrganizer.Collection.AllUsers.Values.ToArray().FirstOrDefault();
            }

            return (data.sender, dat, data.protocol);
        }
    }
}
