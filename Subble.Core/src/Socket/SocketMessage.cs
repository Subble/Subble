namespace Subble.Core.Socket
{
    public class SocketMessage
    {
        public SocketMessage()
        {
            Type = SocketMessageType.OneWay;
            Message = "ECHO";
            Payload = new byte[0];
        }

        /// <summary>
        /// Defines the life span of the socket
        /// </summary>
        public SocketMessageType Type { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// The message paylod, usualy a JSON.NET encoded struct/class
        /// </summary>
        public byte[] Payload { get; set; }
    }
}
