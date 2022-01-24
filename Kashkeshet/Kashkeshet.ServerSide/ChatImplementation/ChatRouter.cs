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
                    var obj = communicator.Receive();

                    communicator.Send(obj);
                }
            }
            catch
            {
                RoutableOrganizer.RemoveUserFromOrganizer(communicator);
            }
        }
    }
}
