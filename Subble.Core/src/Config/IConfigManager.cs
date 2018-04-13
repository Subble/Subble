using Subble.Core.Func;
using System.Threading.Tasks;

namespace Subble.Core.Config
{
    /// <summary>
    /// A key-value store to contain application and plugins settings
    /// </summary>
    public interface IConfigManager
    {
        string ConfigFilePath { get; }

        Option<T> Get<T>(string key);
        void Set<T>(string key, T value);
        bool Delete(string key);
        bool IsAvaible(string key);
    }
}
