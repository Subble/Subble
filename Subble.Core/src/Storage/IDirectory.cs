using System.IO;

namespace Subble.Core.Storage
{
    /// <summary>
    /// Provides an API for Plugins to access the file system
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// Temporary folder, that is frequently deleted
        /// </summary>
        DirectoryInfo TempDirectory { get; }

        /// <summary>
        /// Diretory to store data local to this instalation.
        /// This files should not sync between instances.
        /// </summary>
        DirectoryInfo PrivateDirectory { get; }

        /// <summary>
        /// Directory to store data that may be sync between instances
        /// </summary>
        DirectoryInfo PublicDirectory { get; }

        /// <summary>
        /// Returns the directory reserved to that plugin
        /// </summary>
        /// <param name="pluginGuid">guid of plugin</param>
        /// <param name="isPublic">set if directory should sync between instances</param>
        /// <returns></returns>
        DirectoryInfo GetDirectory(string pluginGuid, bool isPublic = false);

        /// <summary>
        /// Set the default temporary directory
        /// </summary>
        /// <param name="directory"></param>
        bool SetTempDirectory(DirectoryInfo directory);

        /// <summary>
        /// Set the local directory path
        /// </summary>
        /// <param name="directory"></param>
        bool SetPrivateDirectory(DirectoryInfo directory);

        /// <summary>
        /// Set the shared directory path
        /// </summary>
        /// <param name="directory"></param>
        bool SetPublicDirectory(DirectoryInfo directory);
    }
}
