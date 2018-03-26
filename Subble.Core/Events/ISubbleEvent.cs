using Subble.Core.Func;
using System;

namespace Subble.Core.Events
{
    public interface ISubbleEvent
    {
        Guid Id { get; }
        string Type { get; }
        Option Payload { get; }
        DateTime Timestamp { get; }
        string Source { get; }
    }

    public interface ISubbleEvent<T> : ISubbleEvent
    {
        new Option<T> Payload { get; }
    }
}
