using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Store.Presentation.Providers.Logger
{
    public class Logger : ILogger
    {
        private string _path;
        private static object _lock = new object();

        public Logger(string path)
        {
            _path = path;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (exception is null)
            {
                return;
            }

            lock (_lock)
            {
                File.AppendAllText(_path, $"{DateTime.Now}:>{formatter(state, exception)}\nSource:{exception?.Source}\n{exception?.GetType().FullName}->{exception?.Message}\n{exception?.StackTrace ?? string.Empty}\n\n");
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
