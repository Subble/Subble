using System;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Request to send on event
    /// </summary>
    public interface IEventRequest : IRequest
    {
        /// <summary>
        /// Serialization used in payload
        /// </summary>
        byte Serialization { get; }

        /// <summary>
        /// Date time of event
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// Name that identifies event type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// event id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Name of source
        /// </summary>
        string Source { get; }

        /// <summary>
        /// payload
        /// </summary>
        byte[] Payload { get; }
    }
}
