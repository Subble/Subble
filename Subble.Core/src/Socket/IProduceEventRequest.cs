using System;
using System.Collections.Generic;
using System.Text;

namespace Subble.Core.src.Socket
{
    /// <summary>
    /// Request created by client to emit an event
    /// </summary>
    public interface IProduceEventRequest
    {
        /// <summary>
        /// Client identifier
        /// </summary>
        string CientID { get; }

        /// <summary>
        /// Serialization used in payload
        /// </summary>
        byte Serialization { get; }

        /// <summary>
        /// Name that identifies event type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// payload
        /// </summary>
        byte[] Payload { get; }
    }
}
