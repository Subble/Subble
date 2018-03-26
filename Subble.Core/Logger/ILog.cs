using Subble.Core.Func;
using System;

namespace Subble.Core.Logger
{
    public interface ILog
    {
        LogLevel Level { get; }
        string Message { get; }
        DateTime Time { get; }
        Option Details { get; }
    }
}
