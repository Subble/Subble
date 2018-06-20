using System;
using System.Collections.Generic;
using System.Text;

namespace Subble.Core.Socket
{
    /// <summary>
    /// Manage IP and user access
    /// </summary>
    public interface IAccessManager
    {
        /// <summary>
        /// List of IP to allow when whit list is active
        /// </summary>
        IEnumerable<byte[]> IpWhiteList { get; }

        /// <summary>
        /// List of IP to block
        /// </summary>
        IEnumerable<byte[]> IpBlackList { get; }

        /// <summary>
        /// List of users to allow
        /// </summary>
        IEnumerable<string> IdWhiteList { get; }

        /// <summary>
        /// List of users to block
        /// </summary>
        IEnumerable<string> IdBlackList { get; }

        /// <summary>
        /// If true, the white list rules are used instead of black list
        /// </summary>
        bool UseWhiteList { get; }

        /// <summary>
        /// Set if the white list is used
        /// </summary>
        /// <param name="useWhiteList"></param>
        void SetUseWhiteList(bool useWhiteList);

        /// <summary>
        /// Add user to white list
        /// </summary>
        /// <param name="userId"></param>
        void AddToWhiteList(string userId);

        /// <summary>
        /// Add ip to white list
        /// </summary>
        /// <param name="ip"></param>
        void AddToWhiteList(byte[] ip);

        /// <summary>
        /// Add user to black list
        /// </summary>
        /// <param name="userId"></param>
        void AddToBlackList(string userId);

        /// <summary>
        /// Add ip to black list
        /// </summary>
        /// <param name="ip"></param>
        void AddToBlackList(byte[] ip);

        /// <summary>
        /// Remove user from white list
        /// </summary>
        /// <param name="userId"></param>
        void RemoveWhiteList(string userId);

        /// <summary>
        /// Remove ip from white list
        /// </summary>
        /// <param name="ip"></param>
        void RemoveWhiteList(byte[] ip);

        /// <summary>
        /// Remove user from black list
        /// </summary>
        /// <param name="userId"></param>
        void RemoveBlackList(string userId);

        /// <summary>
        /// Remove ip from black list
        /// </summary>
        /// <param name="ip"></param>
        void RemoveBaclkList(byte[] ip);

        /// <summary>
        /// Check if user can access
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool HasAccess(string userId);

        /// <summary>
        /// Check if ip can access
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        bool HasAccess(byte[] ip);

        /// <summary>
        /// Check if client can access
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        bool HasAccess(IClient client);
    }
}
