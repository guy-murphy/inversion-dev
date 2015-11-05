using System;

namespace Inversion.Process
{
    /// <summary>
    /// Represent the contract of creating or adding to a simple
    /// service container
    /// Represent the contract of a simple service from which
    /// services may be obtained by name.
    /// This interface focuses on the configuration of a
    /// container.
    /// </summary>
    public interface IServiceContainerRegistrar : IServiceContainer
    {
        /// <summary>
        /// Registers a singleton service by storing its instance.
        /// </summary>
        /// <typeparam name="T">The type of the service being registered.</typeparam>
        /// <param name="name">The name of the service to register.</param>
        /// <param name="service">The function returning an instance of the service for registration.</param>
        void RegisterService<T>(string name, Func<IServiceContainer, T> service);

        /// <summary>
        /// Registers a non-singleton service by storing its constructor.
        /// </summary>
        /// <typeparam name="T">The type of the service being registered.</typeparam>
        /// <param name="name">The name of the service to register.</param>
        /// <param name="ctor">The function returning a constructor for registration.</param>
        void RegisterServiceNonSingleton<T>(string name, Func<IServiceContainer, T> ctor);
    }
}