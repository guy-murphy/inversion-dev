using System.Collections.Generic;

namespace Inversion.Process.Behaviour {

	/// <summary>
	/// Represents a behaviour that can be configured for use in an application.
	/// </summary>
	public interface IApplicationBehaviour: IProcessBehaviour {
		/// <summary>
		/// Provides access to the behaviours named maps, used to configure
		/// the behaviour.
		/// </summary>
		IDictionary<string, IDictionary<string, string>> NamedMaps { get; }

		/// <summary>
		/// Provides access to the behaviours named lists,
		/// used to configure the behaviour.
		/// </summary>
		IDictionary<string, IEnumerable<string>> NamedLists { get; }

		/// <summary>
		/// Provides acces to the behaviours named map of lists used
		/// to configure the behaviour.
		/// </summary>
		IDictionary<string, IDictionary<string, IEnumerable<string>>> NamedMappedLists { get; }
	}
}