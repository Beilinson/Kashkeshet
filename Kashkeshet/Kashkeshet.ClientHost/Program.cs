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
