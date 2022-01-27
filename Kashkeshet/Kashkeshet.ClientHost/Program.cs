using Kashkeshet.ClientFactories.Implementations;
using Kashkeshet.Common.FileTypes;
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
            var bootstrapper = new Bootstrapper();
            var consoleClient = bootstrapper.CreateConsoleClient();

            await consoleClient.Start();
        }
    }
}
