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
        DirectoryInfo TempFolder { get; }

        /// <summary>
        /// Diretory to store data local to this instalation, 
        /// this files should not sync between instances
        /// </summary>
        DirectoryInfo LocalDirectory { get; }

        /// <summary>
        /// Directory to store data that may be sync between instances
        /// </summary>
        DirectoryInfo SyncDiretory { get; }

        /// <summary>
        /// Returns the directory reserved to that plugin
        /// </summary>
        /// <param name="pluginGuid">guid of plugin</param>
        /// <param name="sync">set if directory should sync between instances</param>
        /// <returns></returns>
        DirectoryInfo GetDirectory(string pluginGuid, bool sync = false);

        /// <summary>
        /// Set the default temporary directory
        /// </summary>
        /// <param name="directory"></param>
        void SetTempFolder(DirectoryInfo directory);

        /// <summary>
        /// Set the local directory path
        /// </summary>
        /// <param name="directory"></param>
        void SetLocalDirectory(DirectoryInfo directory);

        /// <summary>
        /// Set the shared directory path
        /// </summary>
        /// <param name="directory"></param>
        void SetSyncDirectory(DirectoryInfo directory);
    }
}
