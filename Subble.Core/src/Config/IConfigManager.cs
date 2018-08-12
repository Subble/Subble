using Subble.Core.Func;
using System;
using System.Threading.Tasks;

namespace Subble.Core.Config
{
    /// <summary>
    /// A key-value store to contain application and plugins settings
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// Path where the settings are stored
        /// </summary>
        string ConfigFilePath { get; }

        /// <summary>
        /// Attempts to return a value for the specified key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key of requested value</param>
        /// <returns>Value, encapsulated in an optional type</returns>
        Option<T> Get<T>(string key) where T: IConvertible;

        /// <summary>
        /// Attempts to return a value for the specified key
        /// If value does not exist, a default value is set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key of requested value</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T Get<T>(string key, T defaultValue) where T : IConvertible;

        /// <summary>
        /// Sets the value for a key, value is overwritten if exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        void Set<T>(string key, T value) where T: IConvertible;

        /// <summary>
        /// Delete a key-value pair
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>True, if key exists</returns>
        bool Delete(string key);

        /// <summary>
        /// Checks if key is available
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>True if key exists</returns>
        bool IsAvailable(string key);
    }
}
