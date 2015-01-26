using System.Collections.Generic;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// A behaviour responsible for boostrapping the request processing.
	/// Out of the box it simply imports the prameters configured for this behaviour
	/// into the contexts params, so can be seen as a way to configure a context
	/// with default prameters. It should be see as a point of extensibility for
	/// setting up the default state of a context prior to processing a request.
	/// </summary>
	public class BootstrapBehaviour : PrototypedBehaviour {
		/// <summary>
		/// Creates a new instance of the behaviour with no configuration.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		public BootstrapBehaviour(string respondsTo) : base(respondsTo) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public BootstrapBehaviour(string respondsTo, IConfiguration config) : base(respondsTo, config.Elements) {}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour will respond to.</param>
		/// <param name="config">Configuration for the behaviour.</param>
		public BootstrapBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) {}

		/// <summary>
		/// If the conditions of this behaviour are met, copies the parameters
		/// configured for this behaviour into the context parameters, but only if those parameters
		/// aren't already set.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, IProcessContext context) {		
			IDictionary<string, string> mappings = this.Configuration.GetMap("context", "set");
			foreach (KeyValuePair<string, string> entry in mappings) {
				if (!context.HasParams(entry.Key)) {
					context.Params.Add(entry);
				}
			}

		}
	}
}
