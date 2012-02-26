using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    public enum Level
    {
        Fatal,
        Error,
        Warning,
        Debug,
        Trace
    }

    public interface ILogger
    {
        void Log(Level level, string message);
        void Log(Level level, string message, params string[] args);
    }
}
