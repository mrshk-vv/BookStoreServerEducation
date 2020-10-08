using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Store.Presentation.Providers.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        private string _path;

        public LoggerProvider(string path)
        {
            _path = path;
        }
        public void Dispose()
        {
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(_path);
        }
    }
}
