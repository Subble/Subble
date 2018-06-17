namespace Subble.Core.Plugin
{
    /// <summary>
    /// Used by service container to store service instance and version
    /// </summary>
    public struct Implementation
    {
        public Implementation(object value, SemVersion version)
        {
            Value = value;
            Version = version;
        }

        /// <summary>
        /// Instance
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Instance version
        /// </summary>
        public SemVersion Version { get; }
    }
}
