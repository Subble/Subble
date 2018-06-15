using Subble.Core.Func;
using System;

namespace Subble.Core.Socket
{
    public class SubbleSocketException : Exception
    {
        /// <summary>
        /// Message that cause the error
        /// </summary>
        public Option<ISubbleSocketMessage> SocketMessage { get; }

        public SubbleSocketException(Exception innerException)
            : base("Error on push message to server", innerException)
        { }

        public SubbleSocketException(ISubbleSocketMessage message, Exception innerException)
            : base("Error on push message: " + message.Message, innerException)
        { }
    }
}
