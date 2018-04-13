using System;
using System.Collections.Generic;
using System.Text;

namespace Subble.Core.Socket
{
    public enum SocketMessageType
    {
        /// <summary>
        /// Socket is killed after read is completed
        /// </summary>
        OneWay = 1,

        /// <summary>
        /// Socket is killed after response is sent
        /// </summary>
        Duplex = 2,

        /// <summary>
        /// Socket is keeped until the server or client kills it
        /// </summary>
        KeepAlive = 3
    }
}
