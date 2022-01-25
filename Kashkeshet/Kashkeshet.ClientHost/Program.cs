using Kashkeshet.Common.Communicators;
using System;
using System.Threading.Tasks;

namespace Kashkeshet.ClientHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var clientReceiver = bootstrapper.CreateClientReceiver();
            var clientSender = bootstrapper.CreateClientSender();
            var consoleClient = bootstrapper.CreateChatClient(clientReceiver, clientSender);

            await consoleClient.Start();
        }
    }
}
