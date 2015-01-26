using System.Collections.Generic;

namespace Inversion.Process.Behaviour {
	/// <summary>
	/// Describes a configuration that is able to provide
	/// selection criteria suitable for a behaviours condition
	/// based upon what the behaviour expresses in its
	/// configuration.
	/// </summary>
	public interface IPrototype : IConfiguration {
		/// <summary>
		/// The selection criteria that have been chosen for this
		/// prototypes configuration.
		/// </summary>
		IEnumerable<SelectionCriteria> Criteria { get; }
	}
}