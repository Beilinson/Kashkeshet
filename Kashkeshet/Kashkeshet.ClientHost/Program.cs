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

            var receiver = Task.Run(() =>
            {
                client.Start();
            });
            var sender = Task.Run(() =>
            {
                consoleClient.Run();
            });

            Task.WaitAll(receiver, sender);
        }
    }
}
