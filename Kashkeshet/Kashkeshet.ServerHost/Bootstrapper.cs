using Kashkeshet.ServerFactories.Implementations;
using Kashkeshet.ServerSide.Core;

namespace Kashkeshet.ServerHost
{
    public class Bootstrapper
    {
        public IServer CreateChatServer()
        {
            var chatFactory = new ChatFactory();

            var responseAlerts = CreateStandardAlerts();
            var protocolFactory = new ProtocolResponseFactory(responseAlerts);

            var serverFactory = new ServerFactory(chatFactory, protocolFactory);

            return serverFactory.CreateServer();
        }

        public ProtocolResponseAlerts CreateStandardAlerts()
        {
            return new ProtocolResponseAlerts(
                "Is leaving the chat",
                "Created",
                "User has been added",
                "User does not exist",
                "Has entered the chat",
                "Chat does not exist");
        }
    }
}