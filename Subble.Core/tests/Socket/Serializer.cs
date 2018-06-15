#if DEBUG
using Xunit;
using Subble.Core.Socket;
using System.Text;

using static Subble.Core.Socket.SubbleSocketSerializer;

namespace Subble.Core.Test.Socket
{
    public class Serializer
    {
        [Fact]
        public void SerializeSizeMatch()
        {
            var message = new SubbleSocketMessage
            {
                Message = "This is an message",
                Type = SocketMessageType.Duplex
            };

            long textLength = Encoding.UTF8.GetBytes(message.Message).LongLength;
            long totalLength = 3 + 4 + 4 + textLength;

            long calcSize = CalcSize(message);
            long serializeSize = Serialize(message).LongLength;

            Assert.Equal(totalLength, calcSize);
            Assert.Equal(totalLength, serializeSize);
        }

        [Fact]
        public void SerializeSizeMatchOnPayload()
        {
            const string payload = "This is an payload";

            var message = new SubbleSocketMessage
            {
                Message = "This is an message",
                Type = SocketMessageType.Duplex,
                Payload = Encoding.UTF8.GetBytes(payload)
            };

            long textLength = Encoding.UTF8.GetBytes(message.Message).LongLength;
            long totalLength = 3 + 4 + 4 + textLength + message.Payload.LongLength;

            long calcSize = CalcSize(message);
            long serializeSize = Serialize(message).LongLength;

            Assert.Equal(totalLength, calcSize);
            Assert.Equal(totalLength, serializeSize);
        }

        [Fact]
        public void DeserializeMessage()
        {
            const string payload = "This is a random payload";

            var subject = new SubbleSocketMessage
            {
                Type = SocketMessageType.KeepAlive,
                Message = "A service message",
                Payload = Encoding.UTF8.GetBytes(payload)
            };

            var serialized = Serialize(subject);
            var result = Deserialize(serialized);

            Assert.Equal(subject.Type, result.Type);
            Assert.Equal(subject.Message, result.Message);

            var resultPayload = Encoding.UTF8.GetString(result.Payload);
            Assert.Equal(payload, resultPayload);
        }
    }
}
#endif