using Subble.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Serializes a ISubbleMessage to byte array
    /// Rules:
    ///     - 1 Byte  : Major version
    ///     - 1 Byte  : Minor version
    ///     - 1 Byte  : Patch version
    ///     - 4 Byte : Message type
    ///     - 4 Byte : Message Length X
    ///     - X Byte  : Message encoding in UTF-8
    ///     - Remaining bytes are the Payload
    /// </summary>
    public static class SubbleSocketSerializer
    {
        private const byte VERSION_MAJOR = 1;
        private const byte VERSION_MINOR = 0;
        private const byte VERSION_PATCH = 0;

        /// <summary>
        /// Version used to serialize
        /// </summary>
        public static SemVersion Version
            => new SemVersion(
                    VERSION_MAJOR, 
                    VERSION_MINOR, 
                    VERSION_PATCH
                );

        private static Byte[] GetVersion()
        {
            return new byte[]
            {
                VERSION_MAJOR,
                VERSION_MINOR,
                VERSION_PATCH
            };
        }

        public static long CalcSize(ISubbleSocketMessage message)
        {
            var version = GetVersion();
            var type = BitConverter.GetBytes((int)message.Type);
            var messageBytes = Encoding.UTF8.GetBytes(message.Message);
            var messageLength = BitConverter.GetBytes(messageBytes.Length);

            return version.LongLength
                + type.LongLength
                + messageLength.LongLength
                + messageBytes.LongLength
                + (message.Payload?.LongLength ?? 0);
        }

        public static byte[] Serialize(ISubbleSocketMessage message)
        {
            var version = GetVersion();
            var type = BitConverter.GetBytes((int)message.Type);
            var messageBytes = Encoding.UTF8.GetBytes(message.Message);
            var messageLength = BitConverter.GetBytes(messageBytes.Length);

            var result = new List<byte>();
            result.AddRange(version);
            result.AddRange(type);
            result.AddRange(messageLength);
            result.AddRange(messageBytes);

            if (!(message.Payload is null))
                result.AddRange(message.Payload);

            return result.ToArray();
        }

        public static ISubbleSocketMessage Deserialize(byte[] source)
        {
            const int TYPE = 3;
            const int MESSAGE_LENGTH = 7;

            var v = new SemVersion(
                    source[0],
                    source[1],
                    source[2]
                );

            if (!Version.IsCompatible(v))
                throw new Exception("Current deserializer don't suport version: " + v);

            var type = BitConverter.ToInt32(source, TYPE);

            var messageLength = BitConverter.ToInt32(source, MESSAGE_LENGTH);
            var messageBytes = source.Skip(11).Take(messageLength).ToArray();
            var message = Encoding.UTF8.GetString(messageBytes);

            var payloadStart = 11 + messageLength;
            var payload = new byte[0];

            if (source.Length > payloadStart)
                payload = source.Skip(11 + messageLength).ToArray();

            return new SubbleSocketMessage
            {
                Message = message,
                Payload = payload,
                Type = (SocketMessageType)type
            };
        }
    }
}
