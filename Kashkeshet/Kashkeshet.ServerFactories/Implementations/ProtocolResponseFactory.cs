using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerFactories.Abstractions;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashkeshet.ServerFactories.Implementations
{
    public class ProtocolResponseFactory : IProtocolResponseFactory
    {
        public IDictionary<ChatProtocol, ProtocolAction> CreateResponseHandler()
        {
            return new Dictionary<ChatProtocol, ProtocolAction>
            {
                { ChatProtocol.Message, UserNotifyToActiveRoute },
                { ChatProtocol.File, UserNotifyToActiveRoute },
                { ChatProtocol.Audio, UserNotifyToActiveRoute },

                { ChatProtocol.GetAvailableGroups, HandleGroupsRequest },
                { ChatProtocol.LeaveGroup, HandleLeaveRequest },
                { ChatProtocol.RequestUsers, HandleUsersRequest },
                { ChatProtocol.CreateGroup, HandleCreateRequest },
                { ChatProtocol.AddUser, HandleAddUserRequest },
                { ChatProtocol.ChangeGroup, HandleEnterRequest }
            };
        }

        private void HandleLeaveRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            UserNotifyToActiveRoute(controller, communicator, (data.sender, "Is leaving the chat", data.protocol));

            var userData = controller.Collection.AllUsers[communicator];
            controller.Collection.ActiveRoutable[userData] = default;

            communicator.Send((data.sender, "Left Chat", data.protocol));
        }

        private void HandleGroupsRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var userData = controller.Collection.AllUsers[communicator];

            var routes = controller.Collection.UsersInRoutables.Keys
                .Where(route => controller.Collection.UsersInRoutables[route].Contains(userData))
                .ToArray();

            foreach (var route in routes)
            {
                communicator.Send((data.sender, route.ToString(), data.protocol));
            }
        }

        private void HandleUsersRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var message = controller.Collection.AllUsers.Values.ToArray();

            communicator.Send((data.sender, message, data.protocol));
        }

        private void HandleCreateRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            object message = default;
            var userData = controller.Collection.AllUsers[communicator];

            try
            {
                var newRoute = data.message as IRoutable;

                controller.Collection.UsersInRoutables.Add(newRoute, new List<UserData>() { userData });
                controller.Collection.ActiveRoutable[userData] = newRoute;

                message = $"{newRoute} Created";
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            finally
            {
                UserNotifyToActiveRoute(controller, communicator, (data.sender, message, data.protocol));
            }
        }

        private void HandleAddUserRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var mainUser = controller.Collection.AllUsers[communicator];
            var activeRoute = controller.Collection.ActiveRoutable[mainUser];

            var users = controller.Collection.AllUsers.Values;
            foreach (var user in users)
            {
                if (user.ID.ToString() == data.message.ToString() || user.Name == data.message.ToString())
                {
                    controller.Collection.UsersInRoutables[activeRoute].Add(user);
                    UserNotifyToActiveRoute(controller, communicator, (data.sender, $"User {user.ID} has been added", data.protocol));
                    break;
                }
            }

            UserNotifyToActiveRoute(controller, communicator, (data.sender, $"User {data.message} does not exist", data.protocol));

        }

        private void HandleEnterRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var userData = controller.Collection.AllUsers[communicator];
            if (controller.FindRouteByName(data.message.ToString(), out var route))
            {
                controller.Collection.ActiveRoutable[userData] = route;
                UserNotifyToActiveRoute(controller, communicator, (data.sender, "Has entered the chat", data.protocol));
            }
            else
            {
                communicator.Send((data.sender, $"Chat {data.message} does not exist", data.protocol));
            }
        }

        private void UserNotifyToActiveRoute(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            // Active Route:
            var userData = controller.Collection.AllUsers[communicator];
            var currentRoute = controller.Collection.ActiveRoutable[userData];

            if (!communicator.Client.Connected)
            {
                controller.RemoveUser(communicator);
            }
            if (currentRoute != null)
            {
                var activeCommunicators = controller.GetActiveCommunicatorsInRoute(currentRoute);

                // Redistributing message
                currentRoute.UpdateHistory(data);
                EchoMessage(data, activeCommunicators);
            }
            else
            {
                communicator.Send(data);
            }
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
