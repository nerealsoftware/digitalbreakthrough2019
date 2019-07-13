using System.Web.Http.ExceptionHandling;
using NLog;

namespace DB2019.Backend.Api.Helpers
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger ErrorLogger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            ErrorLogger.Error(context.Exception, "Error");
        }
    }
}