using System;

namespace Kashkeshet.ServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();
            var server = bootstrapper.CreateChatServer();
            server.Run();
        }
    }
}
