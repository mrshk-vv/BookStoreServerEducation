using Microsoft.Extensions.Logging;
using Store.Presentation.Providers.Logger;

namespace Store.Presentation.Extentions
{
    public static class LoggerExtentions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string path)
        {
            factory.AddProvider(new LoggerProvider(path));
            return factory;
        }
    }
}
