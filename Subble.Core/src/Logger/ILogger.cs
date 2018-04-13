using Subble.Core.Func;

namespace Subble.Core.Logger
{
    /// <summary>
    /// Service used to emit logs
    /// </summary>
    public interface ILogger
    {
        void Log(LogLevel level, string source, string message);
        void Log(LogLevel level, string source, string message, Option details);

        void LogTrace(string source, string message);
        void LogTrace(string source, string message, Option details);

        void LogDebug(string source, string message);
        void LogDebug(string source, string message, Option details);

        void LogInfo(string source, string message);
        void LogInfo(string source, string message, Option details);

        void LogWarning(string source, string message);
        void LogWarning(string source, string message, Option details);

        void LogError(string source, string message);
        void LogError(string source, string message, Option details);

        void LogFatal(string source, string message);
        void LogFatal(string source, string message, Option details);
    }
}
