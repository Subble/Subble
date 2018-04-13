using System;

namespace Subble.Core.Plugin
{
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

        public Dependency(Type type)
        {
            DependencyType = type;
            Version = new SemVersion(0, 0, 0);
        }


        public Type DependencyType { get; }

        public SemVersion Version { get; }
    }
}
