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

        private void ProcessCommunications(ICommunicator communicator)
        {
            try
            {
                RoutableOrganizer.AddUserToOrganizer(communicator);

                while (true)
                {
                    var message = communicator.Receive();

                    // Active Route:
                    IRoutable route = RoutableOrganizer.Organizer.ActiveRoutable[communicator];
                    var activeUsers = RoutableOrganizer.GetActiveUsersInRoute(route);
                    
                    // Redistributing message
                    route.UpdateHistory(message);
                    EchoMessage(message, activeUsers);
                }
            }
            catch
            {
                Console.WriteLine($"User {communicator.Client} has disconnected");
            }
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
