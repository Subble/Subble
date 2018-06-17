using System.Collections.Generic;

namespace Subble.Core.Plugin
{
    /// <summary>
    /// Metadata for plugin
    /// </summary>
    public interface IPluginInfo
    {
        /// <summary>
        /// Unique string to identify this plugin
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// Plugin name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Plugin creator
        /// </summary>
        string Creator { get; }

        /// <summary>
        /// Repository or webpage URL
        /// </summary>
        string Repository { get; }

        /// <summary>
        /// Email or url where user can get support
        /// </summary>
        string Support { get; }

        /// <summary>
        /// Licence Name
        /// </summary>
        string Licence { get; }
    }
}
