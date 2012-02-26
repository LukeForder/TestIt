using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logging
{
    /// <summary>
    /// An empty logger, its Log methods perform no actions.
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <summary>
        /// Get an instance of a null logger
        /// </summary>
        /// <remarks>
        /// Note this instance is NOT a singleton.
        /// </remarks>
        public static ILogger Instance
        {
            get
            {
                return new NullLogger();
            }
        }

        public void Log(Level level, string message)
        {
        }

        public void Log(Level level, string message, params string[] args)
        {
        }
    }
}
