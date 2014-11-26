using System;

using Spring.Context;
using Spring.Context.Support;

using Inversion.Process;

namespace Inversion.Spring {

	/// <summary>
	/// A service container backed by Sprint.NET
	/// </summary>
	public class ServiceContainer : IServiceContainer {

		private static readonly IServiceContainer _instance = new ServiceContainer();

		/// <summary>
		/// A singleton instance of the service container.
		/// </summary>
		public static IServiceContainer Instance {
			get {
				return _instance;
			}
		}

		private readonly IApplicationContext _container;
		
		/// <summary>
		/// Instantiates a new service container, and configures it
		/// from the Spring config.
		/// </summary>
		/// <remarks>
		/// In most cases you'll probably just want to use `ServiceContainer.Instance`
		/// </remarks>
		public ServiceContainer() {
			_container = ContextRegistry.GetContext();
		}

		/// <summary>
		/// Instantiates a new service container using the
		/// provided application context.
		/// </summary>
		/// <param name="container">
		/// You can think of this `container` as the underlying Spring backing.
		/// This is "the thing".
		/// </param>
		public ServiceContainer(IApplicationContext container) {
			_container = container;
		}

		/// <summary>
		/// Releases all reasources currently being used by this container.
		/// </summary>
		public void Dispose() {
			//
		}

		/// <summary>
		/// Gets the service if any of the provided name.
		/// </summary>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		protected object GetService(string name) {
			return _container.GetObject(name);
		}

		/// <summary>
		/// Gets the service if any of the provided name. Further asserts that the
		/// service is on an expected type.
		/// </summary>
		/// <param name="name">The name of the service to obtain.</param>
		/// <param name="type">The type the service is expected to be.</param>
		/// <returns>Returns the service of the specified name.</returns>
		protected object GetService(string name, Type type) {
			return _container.GetObject(name, type);
		}

		/// <summary>
		/// Gets the service if any of the provided name and type.
		/// </summary>
		/// <typeparam name="T">The type of the service being obtained.</typeparam>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		public T GetService<T>(string name) {
			return (T)this.GetService(name, typeof(T));
		}

		/// <summary>
		/// Determines if the container has a service of a specified name.
		/// </summary>
		/// <param name="name">The name of the service to check for.</param>
		/// <returns>Returns true if the service exists; otherwise returns false.</returns>
		public bool ContainsService(string name) {
			return _container.ContainsObject(name);
		}
	}
}
