using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.ChatImplementation;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kashkeshet.ServerFactories
{
    public class ProtocolResponseFactory
    {
        IDictionary<ChatProtocol, ProtocolAction> CreateResponse()
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
                { ChatProtocol.ChangeGroup, HandleEnterRequest }
            };
        }
        
        public void HandleLeaveRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {

        }

        public void HandleGroupsRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var userData = controller.Collection.AllUsers[communicator];

            var message = controller.Collection.UsersInRoutables.Keys
                .Where(route => controller.Collection.UsersInRoutables[route].Contains(userData))
                .ToArray();

            communicator.Send((data.sender, message, data.protocol));
        }

        public void HandleUsersRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {
            var message = controller.Collection.AllUsers.Values;

            communicator.Send((data.sender, message, data.protocol));
        }

        public void HandleCreateRequest(
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
                communicator.Send((data.sender, message, data.protocol));
            }
            catch (Exception e)
            {
                message = e.Message;
            }
            finally
            {
                communicator.Send((data.sender, message, data.protocol));
            }
        }

        public void HandleEnterRequest(
            IRoutableController controller,
            ICommunicator communicator,
            (object sender, object message, ChatProtocol protocol) data)
        {

        }

        public void UserNotifyToActiveRoute(
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
