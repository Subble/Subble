using System;
using System.Collections.Generic;

namespace Subble.Core.Socket
{
    /// <summary>
    /// A SubbleSocket client
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Client ID
        /// </summary>
        string ID { get; }

        /// <summary>
        /// List of events subscribed
        /// </summary>
        IEnumerable<string> Subscribes { get; }

        /// <summary>
        /// List of events produced
        /// </summary>
        IEnumerable<string> Produces { get; }

        /// <summary>
        /// Date of the last contact to server
        /// </summary>
        DateTime LastContact { get; }

        /// <summary>
        /// IP address used in last location
        /// </summary>
        byte[] IPAddress { get; }
    }
}
