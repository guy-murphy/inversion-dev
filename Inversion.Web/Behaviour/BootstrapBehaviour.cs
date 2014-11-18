using System.Collections.Generic;
using System.Collections.Immutable;

using Inversion.Process;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// A behaviour responsible for boostrapping the request processing.
	/// Out of the box it simply imports the prameters configured for this behaviour
	/// into the contexts params, so can be seen as a way to configure a context
	/// with default prameters. It should be see as a point of extensibility for
	/// setting up the default state of a context prior to processing a request.
	/// </summary>
	public class BootstrapBehaviour : WebBehaviour {

		private readonly ImmutableDictionary<string, string> _params;

		/// <summary>
		/// Gives access to the prameters configured for the bootstrap behaviour
		/// that should be copied into the context params early in the
		/// request life-cycle.
		/// </summary>
		protected IDictionary<string, string> Parameters {
			get { return _params; }
		}

		/// <summary>
		/// Instantiates a new bootstrap behaviour configured with the key-value
		/// pairs provided as parameters.
		/// </summary>
		/// <param name="message">The message this behaviour should respond to.</param>
		/// <param name="parms">The key value-pairs to configure as parameters for this behaviour.</param>
		public BootstrapBehaviour(string message, IEnumerable<KeyValuePair<string, string>> parms)
			: base(message) {
			_params = ImmutableDictionary.CreateRange<string, string>(parms);
		}

		/// <summary>
		/// If the conditions of this behaviour are met, copies the parameters
		/// configured for this behaviour into the context parameters.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, WebContext context) {
			context.Params.Import(this.Parameters);
		}
	}
}
