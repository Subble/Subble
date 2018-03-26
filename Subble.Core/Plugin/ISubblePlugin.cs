using System.Collections.Generic;

namespace Subble.Core.Plugin
{
    /// <summary>
    /// Interface for a plugin
    /// </summary>
    public interface ISubblePlugin
    {
        /// <summary>
        /// Info to register plugin
        /// </summary>
        IPluginInfo Info { get; }

        /// <summary>
        /// Semantic Versioning
        /// </summary>
        SemVersion Version { get; }

        /// <summary>
        /// Load priority, lower values have higher priority
        /// </summary>
        long LoadPriority { get; }

        /// <summary>
        /// List of dependencies required
        /// </summary>
        IEnumerable<Dependency> Dependencies { get; }

        /// <summary>
        /// Called when host initiatizes plugin
        /// </summary>
        /// <param name="host">Caller</param>
        /// <returns>True, if no error occur</returns>
        bool Initialize(ISubbleHost host);
    }
}
