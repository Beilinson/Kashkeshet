using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.User;
using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;
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

        public (object sender, object message, ChatProtocol protocol) HandleProtocol((object sender, object message, ChatProtocol protocol) data)
        {
            throw new NotImplementedException();
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

        private (object, ChatProtocol) CheckIsExecutable(object data, ChatProtocol protocol)
        {
            var dat = data;

            if (protocol == ChatProtocol.DataRequest)
            {
                dat = RoutableController.Collection.AllUsers.Values;
            }

            return (dat, protocol);
        }
    }
}
