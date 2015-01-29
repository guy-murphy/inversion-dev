using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Inversion.Extensions;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {
	/// <summary>
	/// A configuration that is able to provide
	/// selection criteria suitable for a behaviours condition
	/// based upon what the behaviour expresses in its
	/// configuration.
	/// </summary>
	public class WebPrototype: Prototype {

		/// <summary>
		/// The default cases to be used for all prototypes of this class.
		/// </summary>
		public static readonly ConcurrentDictionary<string, IPrototypeCase> NamedWebCases = new ConcurrentDictionary<string, IPrototypeCase>();


		static WebPrototype() {
			// import from parent
			NamedWebCases.Import(NamedCases);
			// then add/override them
			NamedWebCases["match-user-role"] = new Case(
				match: (config) => config.Has("match", "user", "role"),
				criteria: (config, ev) => config.GetValues("match", "user", "role").Any(role => ((IWebContext)ev.Context).User.IsInRole(role))
			);
		}

		/// <summary>
		/// Instantiates a prototype object.
		/// </summary>
		public WebPrototype() : this(new IConfigurationElement[] {}) {}

		/// <summary>
		/// Instantiates a prototype behaviour from the configuration elements and
		/// cases for selection criteria provided.
		/// </summary>
		/// <param name="config">The configuration elements to use for configuration.</param>
		public WebPrototype(IEnumerable<IConfigurationElement> config) : this(config, NamedWebCases.Values) {}

		/// <summary>
		/// Instantiates a prototype behaviour from the configuration elements and
		/// cases for the selection criteria.
		/// </summary>
		/// <param name="config">The configuration elements to use for configuration.</param>
		/// <param name="cases">The cases that should be used for determining selection criteria.</param>
		public WebPrototype(IEnumerable<IConfigurationElement> config, IEnumerable<IPrototypeCase> cases) : base(config, cases) {}
		
	}
}
