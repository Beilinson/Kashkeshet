using System;
using System.Threading.Tasks;

namespace Kashkeshet.ClientHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var client = bootstrapper.CreateClient();
            var consoleClient = bootstrapper.CreateConsoleClient(client);
            client.Start();
            /*Task.Run(() =>
            {
                client.Start();
            });
            Task.Run(() =>
            {
                consoleClient.Run();
            });*/
        }
    }
}
