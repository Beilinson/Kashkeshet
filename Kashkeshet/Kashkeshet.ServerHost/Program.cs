using Kashkeshet.Common.Logging;

namespace Kashkeshet.ServerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Instance.Log.Info("Server Entry Point");

            var bootstrapper = new Bootstrapper();
            var server = bootstrapper.CreateChatServer();

            server.Run();
        }
    }
}
