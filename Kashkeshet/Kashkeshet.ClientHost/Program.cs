using Kashkeshet.ClientFactories.Implementations;
using Kashkeshet.Common.Loaders;
using Kashkeshet.ConsoleUI;
using Kashkeshet.ServerFactories;
using Kashkeshet.ServerFactories.Implementations;
using System.Threading.Tasks;

namespace Kashkeshet.ClientHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new ConsoleInput();
            var ouput = new ConsoleOutput();
            var fileLoader = new FileLoader();
            var chatFactory = new ChatFactory();

            var clientFactory = new ConsoleClientFactory(input, ouput, fileLoader);
            var protocolFactory = new ProtocolRequestFactory(input, ouput, fileLoader, chatFactory);

            var bootstrapper = new Bootstrapper(clientFactory, protocolFactory);
            var consoleClient = bootstrapper.CreateClient();

            await consoleClient.Start();
        }
    }
}
