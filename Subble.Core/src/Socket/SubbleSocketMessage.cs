using System;

namespace Subble.Core.Socket
{
    public class SubbleSocketMessage :  ISubbleSocketMessage
    {
        public SocketMessageType Type { get; set; }

        public string Message { get; set; }

        public Byte[] Payload { get; set; }
    }
}
