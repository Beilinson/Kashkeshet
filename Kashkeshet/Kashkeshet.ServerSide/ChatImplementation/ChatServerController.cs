using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashkeshet.ServerSide.ChatImplementation
{
    public class ChatServerController : IServerController
    {
        public IRoutableController RoutableController { get; }

        public ChatServerController(IRoutableController routableController)
        {
            RoutableController = routableController;
        }

        public void HandleProtocol(ICommunicator user, (object sender, object message, ChatProtocol protocol) data)
        {
            // Very temporary, will replace later.
            object newMessage;
            var userData = RoutableController.Collection.AllUsers[user];
            var currentRoute = RoutableController.Collection.ActiveRoutable[userData];

            switch (data.protocol)
            {
                case ChatProtocol.CreateGroup:
                    try
                    {
                        RoutableController.Collection.UsersInRoutables.Add((IRoutable)data.message, new List<UserData>() { userData });
                        newMessage = RoutableController.GetActiveUsersInRoute(currentRoute);
                    }
                    catch (Exception e)
                    {
                        newMessage = e.Message;
                    }
                    break;
                case ChatProtocol.RequestUsers:
                    newMessage = RoutableController.GetActiveUsersInRoute(currentRoute);
                    break;
                case ChatProtocol.GetAvailableGroups:
                    newMessage = RoutableController.GetActiveUsersInRoute(currentRoute);
                    break;
                case ChatProtocol.LeaveGroup:
                    RoutableController.Collection.ActiveRoutable[userData] = default;
                    newMessage = RoutableController.Collection.UsersInRoutables.Keys
                        .Where(route => RoutableController.Collection.UsersInRoutables[route].Contains(userData));
                    break;
                case ChatProtocol.ChangeGroup:
                    try
                    {
                        if (RoutableController.FindRouteByName(data.message.ToString(), out var route))
                        {
                            if (RoutableController.Collection.UsersInRoutables[route].Contains(userData))
                            {
                                RoutableController.Collection.ActiveRoutable[userData] = route;
                                foreach (var history in currentRoute.MessageHistory.GetHistory())
                                {
                                    user.Send(history);
                                }
                            }
                        }
                        newMessage = data.message;
                    } 
                    catch (Exception e)
                    {
                        newMessage = e;
                    }
                    break;
                default:
                    newMessage = data.message;
                    UserNotifyToActiveRoute(user, data);
                    break;
            };

            user.Send((data.sender, newMessage, data.protocol));
        }

        public void RevealHistory(ICommunicator user)
        {
            var userData = RoutableController.Collection.AllUsers[user];
            var currentRoute = RoutableController.Collection.ActiveRoutable[userData];
            foreach (var data in currentRoute.MessageHistory.GetHistory())
            {
                user.Send(data);
            }
        }

        public void UserNotifyToActiveRoute(ICommunicator user, (object, object, ChatProtocol) data)
        {
            // Active Route:
            var userData = RoutableController.Collection.AllUsers[user];
            var currentRoute = RoutableController.Collection.ActiveRoutable[userData];

            if (!user.Client.Connected)
            {
                RoutableController.RemoveUser(user);
            }

            var activeCommunicators = RoutableController.GetActiveCommunicatorsInRoute(currentRoute);

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
    }
}
