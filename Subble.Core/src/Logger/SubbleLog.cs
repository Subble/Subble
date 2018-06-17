using Subble.Core.Func;
using System;

namespace Subble.Core.Logger
{
    /// <summary>
    /// ILog default implementation,
    /// contains helper methods to create log entries
    /// </summary>
    public class SubbleLog : ILog
    {
        public SubbleLog()
            => Details = Option.None();

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }

        public Option Details { get; set; }

        public static SubbleLog GetLog(LogLevel level, string message)
            => new SubbleLog { Level = level, Message = message, Time = DateTime.Now };

        public static SubbleLog GetTraceLog(string message)
            => GetLog(LogLevel.Trace, message);

        public static SubbleLog GetDebugLog(string message)
            => GetLog(LogLevel.Debug, message);

        public static SubbleLog GetInfoLog(string message)
            => GetLog(LogLevel.Info, message);

        public static SubbleLog GetWarningLog(string message)
            => GetLog(LogLevel.Warning, message);

        public static SubbleLog GetErrorLog(string message)
            => GetLog(LogLevel.Error, message);

        public static SubbleLog GetFatalLog(string message)
            => GetLog(LogLevel.Fatal, message);

        public override string ToString()
        {
            return $"[{Time.ToString("yyyy-MM-dd|hh:mm:ss")}]{Level}|{Message}";
        }
    }
}