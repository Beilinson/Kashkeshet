using Kashkeshet.ServerFactories.Implementations;
using Kashkeshet.ServerSide.Core;

namespace Kashkeshet.ServerHost
{
    public class Bootstrapper
    {
        public IServer CreateChatServer()
        {
            var chatFactory = new ChatFactory();
            var protocolFactory = new ProtocolResponseFactory();

            var serverFactory = new ServerFactory(chatFactory, protocolFactory);

            return serverFactory.CreateServer();
        }
    }
}