using System;

namespace Subble.Core.Plugin
{
    /// <summary>
    /// Declares a dependency, used by host to reoder load priority
    /// </summary>
    public class Dependency
    {
        public Dependency(Type type, uint major, uint minor, uint patch)
        {
            Version = new SemVersion(major, minor, patch);
            DependencyType = type;
        }

        public Dependency(Type type, SemVersion version)
        {
            DependencyType = type;
            Version = version;
        }

        /// <summary>
        /// Service type
        /// </summary>
        public Type DependencyType { get; }

        /// <summary>
        /// required comptability with version
        /// </summary>
        public SemVersion Version { get; }
    }
}
