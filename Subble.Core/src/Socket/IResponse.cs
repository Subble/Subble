namespace Subble.Core.Socket
{
    /// <summary>
    /// Server response after handshake request or when error occur
    /// </summary>
    public interface IResponse : IRequest
    {
        /// <summary>
        /// Response message
        /// </summary>
        string Message { get; }
    }
}
