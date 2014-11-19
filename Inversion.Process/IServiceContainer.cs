using System;

namespace Inversion.Process {

	/// <summary>
	/// Represent the contract of a simple service
	/// container from which services may be ontained by name.
	/// This interface focused on consuming services from a container
	/// and does not speak to the configuration or management
	/// of a container.
	/// </summary>
	public interface IServiceContainer : IDisposable {
		/// <summary>
		/// Gets the service if any of the provided name.
		/// </summary>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		object this[string name] { get; }
		/// <summary>
		/// Gets the service if any of the provided name.
		/// </summary>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		object GetObject(string name);
		/// <summary>
		/// Gets the service if any of the provided name. Further asserts that the
		/// service is on an expected type.
		/// </summary>
		/// <param name="name">The name of the service to obtain.</param>
		/// <param name="type">The type the service is expected to be.</param>
		/// <returns>Returns the service of the specified name.</returns>
		object GetObject(string name, Type type);
		/// <summary>
		/// Gets the service if any of the provided name and type.
		/// </summary>
		/// <typeparam name="T">The type of the service being obtained.</typeparam>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		T GetObject<T>(string name);
		/// <summary>
		/// Determines if the container has a service of a specified name.
		/// </summary>
		/// <param name="name">The name of the service to check for.</param>
		/// <returns>Returns true if the service exists; otherwise returns false.</returns>
		bool ContainsObject(string name);
	}
}
