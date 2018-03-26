using Subble.Core.Func;
using System;

namespace Subble.Core.ServiceContainer
{
    /// <summary>
    /// Used on Register/Remove events
    /// </summary>
    public class SCPayload
    {
        public SCPayload(Type type, object service, bool isOverride = false)
        {
            ServiceType = type;
            Service = Option.Some(service);
            IsOverride = isOverride;
        }

        /// <summary>
        /// Type to register
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Implementation
        /// </summary>
        public Option Service { get; }

        /// <summary>
        /// True, if a service override occur
        /// </summary>
        public bool IsOverride { get; }

        public static SCPayload Create<T>(T service, bool isOverride = false)
        {
            return new SCPayload(typeof(T), service, isOverride);
        }
    }
}
