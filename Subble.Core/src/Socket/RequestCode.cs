using System;
using System.Collections.Generic;
using System.Text;

namespace Subble.Core.Socket
{
    public enum RequestCode
    {
        /// <summary>
        ///  The code sent in the request to start the handshake
        /// </summary>
        HandshakeRequest = 1,

        /// <summary>
        /// The handshake is completed and the server is ok to receive new request
        /// </summary>
        HandshakeCompleted = 2,

        /// <summary>
        /// New User ID created: The handshake is completed and the server has successfully 
        /// created a new client ID, check the message for the client ID
        /// </summary>
        NewUserCreated = 3,

        /// <summary>
        /// IP not allowed: Server is actively denying the client IP, 
        /// check a possible whitelist or blacklist
        /// </summary>
        IPBlocked = 10,

        /// <summary>
        /// New clients not allowed: The Client ID was not defined and 
        /// the server has the generation of new client ID disabled
        /// </summary>
        NewClientsNotAllowed = 11,

        /// <summary>
        /// Invalid client ID: The Client was defined, but the server don't recognize it
        /// </summary>
        InvalidClientID = 12,

        /// <summary>
        /// Server refused the connection for unknown reasons
        /// </summary>
        UnkownHandshakeError = 25,

        /// <summary>
        /// Event created in server side, events created by other clients will also have this code
        /// </summary>
        ServerEvent = 26,

        /// <summary>
        /// Event created by client
        /// </summary>
        ClientEvent = 27,

        /// <summary>
        /// Event was not created by server??
        /// </summary>
        UnkownEventError = 52,

        /// <summary>
        /// Event name was not defined during handshake
        /// </summary>
        EventNotDeclared = 51
    }
}
