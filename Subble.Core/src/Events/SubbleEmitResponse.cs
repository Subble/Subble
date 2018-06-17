using System;

namespace Subble.Core.Events
{
    /// <summary>
    /// Details of the emited event,
    /// handy to debug
    /// </summary>
    public struct SubbleEmitResponse
    {
        public SubbleEmitResponse(bool isError, string message, Guid id)
        {
            IsError = isError;
            Message = message;
            Id = id;
        }

        /// <summary>
        /// If true, an error occur when pushing the event
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// Error details
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The event id
        /// </summary>
        public Guid Id { get; set; }
    }
}
