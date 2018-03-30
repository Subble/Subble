using Subble.Core.Func;
using Subble.Core.Events;
using Subble.Core.Plugin;
using System;
using System.Collections.Generic;

namespace Subble.Core.ServiceContainer
{
    /// <summary>
    /// Manage known services
    /// </summary>
    public interface IServiceContainer
    {
        /// <summary>
        /// Collection of implementations
        /// </summary>
        IEnumerable<Implementation> ServiceImplementations { get; }
        /// <summary>
        /// Collection of services types
        /// </summary>
        IEnumerable<Type> ServiceTypes { get; }
        /// <summary>
        /// Get service of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type to search</typeparam>
        /// <returns>default <see cref="T"/> if not found</returns>
        Option<T> GetService<T>();
        /// <summary>
        /// Check if service exists
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>True if found, false otherwise</returns>
        bool HasService<T>();
        bool HasService<T>(out T value);
        /// <summary>
        /// Registers a new service in container
        /// </summary>
        /// <typeparam name="T">Type of service to register</typeparam>
        /// <param name="serviceImplementation">service implementation</param>
        /// <param name="version">version of implementation</param>
        /// <returns>True if successfully register, false otherwise</returns>
        bool RegisterService<T>(object serviceImplementation, SemVersion version);
        /// <summary>
        /// Remove a service implementation
        /// </summary>
        /// <typeparam name="T">Type of service</typeparam>
        /// <returns>True if removed, false otherwise</returns>
        bool RemoveService<T>();

        /// <summary>
        /// Check if service implementation is present and is version compatible
        /// </summary>
        /// <typeparam name="T">type of service</typeparam>
        /// <param name="version">min version of service</param>
        /// <returns>0 if not present, -1 if versions are incompatible and 1 if OK</returns>
        int MatchService<T>(SemVersion version);

        /// <summary>
        /// Checks if required dependency is present
        /// </summary>
        /// <param name="dependency"></param>
        /// <returns>0 if not present, -1 if versions are incompatible and 1 if OK</returns>
        int ValidateDependency(Dependency dependency);

        /// <summary>
        /// Service container events
        /// </summary>
        IObservable<SubbleEvent<SCPayload>> Events { get; }
    }
}
