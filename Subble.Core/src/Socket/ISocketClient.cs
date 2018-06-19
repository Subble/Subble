using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Interface to communicate with socket clients
    /// </summary>
    public interface ISocketClient : IClient, IDisposable
    {
        /// <summary>
        /// True if client is connected
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Close socket
        /// </summary>
        /// <returns></returns>
        Task Close();

        /// <summary>
        /// Send message to client
        /// </summary>
        /// <param name="data">message to send</param>
        /// <returns></returns>
        Task Send(byte[] data);
    }
}
