using System.Threading.Tasks;

namespace Subble.Core.Socket
{
    public interface ISubbleSocketServer
    {
        /// <summary>
        /// Start listening for connections
        /// </summary>
        /// <returns>returns the port used to listening</returns>
        Task<int> Start();


        /// <summary>
        /// Close all connections and stop listening
        /// </summary>
        /// <returns></returns>
        Task Close();
    }
}
