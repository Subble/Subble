using System;
using Subble.Core.Events;
using Subble.Core.Plugin;
using Subble.Core.ServiceContainer;

namespace Subble.Core
{
    public interface ISubbleHost
    {
        /// <summary>
        /// Current version of Subble
        /// </summary>
        SemVersion Version { get; }

        /// <summary>
        /// Observer of events
        /// </summary>
        IObservable<ISubbleEvent> Events { get; }

        /// <summary>
        /// Entry point to handle services
        /// </summary>
        IServiceContainer ServiceContainer { get; }

        SubbleEmitResponse EmitEvent(string type, string source);
        SubbleEmitResponse EmitEvent<T>(string type, string source, T payload);
    }
}
