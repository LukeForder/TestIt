using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    public class BlkBxLogger : ILogger
    {
        public BlkBxLogger(BlackBox.ILogger logger)
        {
            _logger = logger;
        }

        private BlackBox.ILogger _logger;

        public void Log(Level level, string message)
        {
            _logger.Write(TranslateLevel(level), message);
        }

        public void Log(Level level, string message, params string[] args)
        {
            _logger.Write(TranslateLevel(level), string.Format(message, args));
        }

        protected BlackBox.LogLevel TranslateLevel(Level level)
        {
            switch (level)
            {
                case Level.Trace:
                    return BlackBox.LogLevel.Verbose;
                case Level.Debug:
                    return BlackBox.LogLevel.Information;
                case Level.Warning:
                    return BlackBox.LogLevel.Warning;
                case Level.Error:
                    return BlackBox.LogLevel.Error;
                case Level.Fatal:
                    return BlackBox.LogLevel.Fatal;
                default:
                    return BlackBox.LogLevel.Error;
            }
        }
    }
}
