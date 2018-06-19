using Subble.Core.Plugin;
using System.Collections.Generic;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Request made by client when attemps a connection with server
    /// </summary>
    public interface IHandshakeRequest : IRequest
    {
        /// <summary>
        /// Subble protocol message used by client
        /// </summary>
        SemVersion Version { get; }

        /// <summary>
        /// Client Identifier
        /// </summary>
        string ClientID { get; }

        /// <summary>
        /// Value used to separate values
        /// </summary>
        string Separator { get; }

        /// <summary>
        /// List of events subscribe by client
        /// </summary>
        IEnumerable<string> Subscribe { get; }

        /// <summary>
        /// List of events that the client can emit
        /// </summary>
        IEnumerable<string> Produce { get; }
    }
}
