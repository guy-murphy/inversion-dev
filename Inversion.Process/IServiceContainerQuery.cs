using System;
using System.Collections.Generic;

namespace Inversion.Process
{
    /// <summary>
    /// Perform some simple queries upon a service container.
    /// </summary>
    public interface IServiceContainerQuery : IServiceContainer
    {
        /// <summary>
        /// Query the service container for service names whose types match a particular cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singletons"></param>
        /// <returns>IEnumerable of service names.</returns>
        IEnumerable<string> GetServiceNamesOfType<T>(bool singletons = true) where T : class;
    }
}