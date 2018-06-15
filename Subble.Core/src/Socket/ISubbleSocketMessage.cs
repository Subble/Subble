using Subble.Core.Func;
using System;

namespace Subble.Core.Socket
{
    public interface ISubbleSocketMessage
    {
        /// <summary>
        /// Defines the life span of the socket
        /// </summary>
        SocketMessageType Type { get; }

        /// <summary>
        /// Message to/from server
        /// </summary>
        string Message { get; }

        /// <summary>
        /// The message paylod
        /// </summary>
        Byte[] Payload { get; }
    }
}
