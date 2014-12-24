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
		IDictionary<string, IDictionary<string, string>> NamedMaps { get; set; }

		/// <summary>
		/// Provides access to the behaviours named lists,
		/// used to configure the behaviour.
		/// </summary>
		IDictionary<string, IEnumerable<string>> NamedLists { get; set; }
	}
}