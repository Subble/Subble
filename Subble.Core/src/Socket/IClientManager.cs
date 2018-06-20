using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Manage acces from remote clients, allow user to black IP, and clients
    /// </summary>
    public interface IClientManager
    {
        /// <summary>
        /// Whether new clients are allowed
        /// </summary>
        bool AllowNewClient { get; }

        /// <summary>
        /// Set whether new clients are allowed
        /// </summary>
        /// <param name="allow">Value to define if new clients are allowed</param>
        void SetAllowNewClient(bool allow);

        /// <summary>
        /// Return all clients
        /// </summary>
        /// <returns></returns>
        IEnumerable<IClient> GetClients();

        /// <summary>
        /// Add ip and id to block list
        /// </summary>
        /// <param name="client"></param>
        void BlockClient(IClient client);

        /// <summary>
        /// Check if client is blocked
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        bool HasAccess(IClient client);

        /// <summary>
        /// Add a new client to list, an id is created for that client
        /// </summary>
        /// <param name="client">The client to insert</param>
        /// <returns></returns>
        IClient InsertClient(IClient client);
    }
}
