using System;
using Subble.Core.Events;
using Subble.Core.Plugin;
using Subble.Core.ServiceContainer;

namespace Subble.Core
{
    /// <summary>
    /// It's the one responsible to manage the plugins and 
    /// inject the required dependencies
    /// </summary>
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

        /// <summary>
        /// Creates an event that is sent to all subscribers of this event type
        /// </summary>
        /// <param name="type">Type of event, used to filter subscribers</param>
        /// <param name="source">The event emiter, usualy the plugin name that creates this event</param>
        /// <returns>Details of event</returns>
        SubbleEmitResponse EmitEvent(string type, string source);

        /// <summary>
        /// Creates an event that is sent to all subscribers of this event type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Type of event, used to filter subscribers</param>
        /// <param name="source">The event emiter, usualy the plugin name that creates this event</param>
        /// <param name="payload">Extra info to be included in the event</param>
        /// <returns>Details of event</returns>
        SubbleEmitResponse EmitEvent<T>(string type, string source, T payload);
    }
}
