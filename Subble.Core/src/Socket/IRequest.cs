using System;
using System.Collections.Generic;
using System.Text;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Base interface for all requests
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// The code that identifies the request
        /// </summary>
        RequestCode RequestCode { get; }

        /// <summary>
        /// Extra data sent by a client, with the request is server fresh, this value is empty
        /// </summary>
        byte[] RequestData { get; }
    }
}