using Subble.Core.Func;
using System;

namespace Subble.Core.Logger
{
    /// <summary>
    /// Log params
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Log level
        /// </summary>
        LogLevel Level { get; }

        /// <summary>
        /// Log message
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Time stamp
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// log details, can be any information, handy to debug
        /// </summary>
        Option Details { get; }
    }
}
