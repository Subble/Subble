using Subble.Core.Func;
using System;

namespace Subble.Core.Events
{
    /// <summary>
    /// An Event
    /// </summary>
    public interface ISubbleEvent
    {
        /// <summary>
        /// Event unique id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Type of event, used by subscribers to 
        /// filter events
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Paylod of event
        /// </summary>
        Option Payload { get; }

        /// <summary>
        /// Emit time
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Event emiter, usualy plugin name
        /// </summary>
        string Source { get; }
    }

    public interface ISubbleEvent<T> : ISubbleEvent
    {
        new Option<T> Payload { get; }
    }
}
