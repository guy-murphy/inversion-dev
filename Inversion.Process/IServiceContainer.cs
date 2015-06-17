using System;

namespace Inversion.Process {

	/// <summary>
	/// Represent the contract of a simple service
	/// container from which services may be obtained by name.
	/// This interface is focused on consuming services from a
	/// container and does not speak to the configuration or
	/// management of a container.
	/// </summary>
	public interface IServiceContainer : IDisposable {
		/// <summary>
		/// Gets the service if any of the provided name and type.
		/// </summary>
		/// <typeparam name="T">The type of the service being obtained.</typeparam>
		/// <param name="name">The name of the service to obtain.</param>
		/// <returns>Returns the service of the specified name.</returns>
		T GetService<T>(string name) where T: class;
		/// <summary>
		/// Determines if the container has a service of a specified name.
		/// </summary>
		/// <param name="name">The name of the service to check for.</param>
		/// <returns>Returns true if the service exists; otherwise returns false.</returns>
		bool ContainsService(string name);
	}
}