using Subble.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Server of SubbleSocket
    /// </summary>
    public interface ISocketServer
    {
        /// <summary>
        /// Start socket server
        /// </summary>
        /// <returns>True, if the server is running</returns>
        Task<bool> Start();

        /// <summary>
        /// Close all socket connections
        /// </summary>
        /// <returns></returns>
        Task<bool> Close();

        /// <summary>
        /// Indicates if the server is running
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Check if middleware is present
        /// </summary>
        /// <param name="id">Middleware id</param>
        /// <returns>True, if is present</returns>
        bool HasMiddleware(Guid id);

        /// <summary>
        /// Adds middleware to proccess incoming events
        /// </summary>
        /// <param name="middleware">Midleware function</param>
        /// <returns>the GUID, that can be used to remove the midleware</returns>
        Guid AddMiddleware(Func<byte[], byte[]> middleware);

        /// <summary>
        /// Removes a midleware
        /// </summary>
        /// <param name="id">id to remove</param>
        /// <returns>False, if id is not present</returns>
        bool RemoveMiddleware(Guid id);

        /// <summary>
        /// Send an request to all clients, this should be used with caution
        /// since all clients will receive, and a filter cannot be applied
        /// </summary>
        /// <param name="request">request to be sent</param>
        /// <returns>True if sent</returns>
        Task<bool> SendRequest(IRequest request);

        /// <summary>
        /// Collection of connected clients
        /// </summary>
        IEnumerable<ISocketClient> ConnectedClients { get; }
    }
}