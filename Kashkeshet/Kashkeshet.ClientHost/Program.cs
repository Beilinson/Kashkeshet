using Kashkeshet.ConsoleUI;
using System.Threading.Tasks;

namespace Kashkeshet.ClientHost
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new ConsoleInput();
            var output = new ConsoleOutput();

            var bootstrapper = new Bootstrapper(input, output);
            var consoleClient = bootstrapper.CreateConsoleClient();

            bootstrapper.ExplainProgram();

            await consoleClient.Start();
        }
    }
}
