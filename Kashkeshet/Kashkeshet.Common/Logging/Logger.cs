using System;
using log4net;

namespace Kashkeshet.Common.Logging
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance => lazy.Value;

        public ILog Log { get; }

        private Logger() 
        {
            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
