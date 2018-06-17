using System;
using System.Threading.Tasks;

namespace Subble.Core.Socket
{
    public interface ISubbleSocket
    {
        /// <summary>
        /// Wheather socket connection is open or close
        /// </summary>
        /// <returns>true, if connection is open</returns>
        bool IsAlive { get; }
        
        /// <summary>
        /// Endpoint used to connect socket
        /// </summary>
        /// <returns></returns>
        string Endpoint { get; }

        /// <summary>
        /// Get or Set if an error is thrown when an error occur
        /// </summary>
        bool ThrowOnError { get; set; }

        /// <summary>
        /// Close connection
        /// </summary>
        /// <returns></returns>
        Task Close();

        /// <summary>
        /// Send message to server
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task PushMessage(ISubbleSocketMessage message);

        /// <summary>
        /// Errors that occur in this socket
        /// </summary>
        IObservable<SubbleSocketException> Errors { get; }

        /// <summary>
        /// Received messages
        /// </summary>
        IObservable<ISubbleSocketMessage> Messages { get; }

        event Action OnClose;
    }
}